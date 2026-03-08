using System;
using System.Diagnostics;
using System.IO;
using System.Windows;
using System.Windows.Shapes;
using System.IO;
using Path = System.IO.Path;
using MessageBox = System.Windows.MessageBox;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
using System.Windows.Controls;
using Microsoft.Win32;

namespace ShortcutManager
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }


        // ==========================================
        // SEKME 1: SIFIRDAN KISAYOL OLUŞTURMA (EXE)
        // ==========================================
        private void BrowseFolder_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFolderDialog { Title = "Uygulama Klasörünü Seçin" };
            if (dialog.ShowDialog() == true)
            {
                FolderPathTextBox.Text = dialog.FolderName;
            }
        }

        private void BrowseIcon_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Title = "Özel Kısayol İkonunu Seçin (.ico veya .exe)",
                Filter = "İkon Kaynakları (*.ico;*.exe)|*.ico;*.exe|İkon Dosyaları (*.ico)|*.ico|Uygulama (*.exe)|*.exe"
            };

            if (dialog.ShowDialog() == true)
            {
                CustomIconPathTextBox.Text = dialog.FileName;
            }
        }

        private System.Collections.Generic.List<ExeFolderGroup> _allGroups = new();

        private void FolderPathTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string path = FolderPathTextBox.Text.Trim();

            if (Directory.Exists(path))
            {
                if (string.IsNullOrEmpty(ShortcutNameTextBox.Text))
                {
                    DirectoryInfo dirInfo = new DirectoryInfo(path);
                    ShortcutNameTextBox.Text = dirInfo.Name;
                }

                int depth = (int)(DepthSlider?.Value ?? 3);
                LoadExeFilesInFolder(path, depth);
            }
            else
            {
                _allGroups.Clear();
                ExeFilesTreeView.ItemsSource = null;
            }
        }

        private void DepthSlider_ValueChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<double> e)
        {
            string path = FolderPathTextBox?.Text?.Trim();
            if (!string.IsNullOrEmpty(path) && Directory.Exists(path))
            {
                LoadExeFilesInFolder(path, (int)e.NewValue);
            }
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string query = SearchTextBox.Text.Trim().ToLowerInvariant();
            ApplySearchFilter(query);
        }

        private void ApplySearchFilter(string query)
        {
            if (_allGroups == null) return;

            if (string.IsNullOrEmpty(query))
            {
                ExeFilesTreeView.ItemsSource = null;
                ExeFilesTreeView.ItemsSource = _allGroups;
                return;
            }

            var filtered = new System.Collections.Generic.List<ExeFolderGroup>();
            foreach (var group in _allGroups)
            {
                // Match folder name or any item filename
                var matchingItems = group.Items
                    .Where(i => i.FileName.ToLowerInvariant().Contains(query) || group.FolderName.ToLowerInvariant().Contains(query))
                    .ToList();

                if (matchingItems.Count > 0)
                {
                    filtered.Add(new ExeFolderGroup { FolderName = group.FolderName, Items = matchingItems });
                }
            }

            ExeFilesTreeView.ItemsSource = null;
            ExeFilesTreeView.ItemsSource = filtered;
        }

        private void LoadExeFilesInFolder(string folderPath, int maxDepth)
        {
            try
            {
                _allGroups.Clear();
                ExeFilesTreeView.ItemsSource = null;

                var searchOptions = new EnumerationOptions
                {
                    IgnoreInaccessible = true,
                    RecurseSubdirectories = true,
                    MaxRecursionDepth = maxDepth,
                    AttributesToSkip = FileAttributes.None
                };

                string[] exeFiles = Directory.GetFiles(folderPath, "*.exe", searchOptions);

                // Group by immediate subfolder
                var groups = new System.Collections.Generic.Dictionary<string, ExeFolderGroup>();

                foreach (string exe in exeFiles)
                {
                    string relativePath = exe.Substring(folderPath.Length).TrimStart('\\');
                    string[] parts = relativePath.Split('\\');

                    // Folder key: everything except the filename
                    string folderKey = parts.Length > 1
                        ? string.Join("\\", parts, 0, parts.Length - 1)
                        : "Ön Klasör";

                    if (!groups.ContainsKey(folderKey))
                    {
                        string displayName = parts.Length > 1 ? parts[0] : "Ön Klasör";
                        // Show last meaningful segment for deep paths
                        if (parts.Length > 2) displayName = folderKey;
                        groups[folderKey] = new ExeFolderGroup { FolderName = displayName };
                    }

                    groups[folderKey].Items.Add(new ExeFileItem
                    {
                        FullPath = exe,
                        RelativePath = relativePath,
                        FileName = Path.GetFileName(exe),
                        IsSelected = false
                    });
                }

                _allGroups = groups.Values.ToList();
                // Auto-expand all folders and select first exe if only one group
                if (_allGroups.Count == 1 && _allGroups[0].Items.Count > 0)
                    _allGroups[0].Items[0].IsSelected = true;

                ExeFilesTreeView.ItemsSource = null;
                ExeFilesTreeView.ItemsSource = _allGroups;

                // Auto-expand all nodes
                foreach (var item in ExeFilesTreeView.Items)
                {
                    var tvi = ExeFilesTreeView.ItemContainerGenerator.ContainerFromItem(item) as TreeViewItem;
                    if (tvi != null) tvi.IsExpanded = true;
                }
            }
            catch (Exception) { }
        }

        private void CreateShortcut_Click(object sender, RoutedEventArgs e)
        {
            string folderPath = FolderPathTextBox.Text.Trim();
            string customPrefix = ShortcutNameTextBox.Text.Trim();

            if (string.IsNullOrEmpty(folderPath) || !Directory.Exists(folderPath))
            {
                MessageBox.Show("Łütfen geçerli bir klasör yolu girin.", "Uyarı", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            // TreeView’dan tüm seçili itemlerı topla
            var selectedItems = new System.Collections.Generic.List<ExeFileItem>();
            var groups = ExeFilesTreeView.ItemsSource as System.Collections.Generic.IEnumerable<ExeFolderGroup>;
            if (groups != null)
            {
                foreach (var group in groups)
                    foreach (var item in group.Items)
                        if (item.IsSelected) selectedItems.Add(item);
            }

            if (selectedItems.Count == 0)
            {
                MessageBox.Show("Lütfen listeden en az bir çalıştırılabilir dosya (.exe) seçin.", "Uyarı", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int successCount = 0;

            foreach (var selectedExeItem in selectedItems)
            {
                string originalTargetExePath = selectedExeItem.FullPath;
                string exeNameWithoutExt = Path.GetFileNameWithoutExtension(originalTargetExePath);
                
                // Kısayol adı belirleme (Prefix varsa ekle)
                string shortcutName = string.IsNullOrEmpty(customPrefix) 
                                        ? exeNameWithoutExt 
                                        : (selectedItems.Count > 1 ? $"{customPrefix} - {exeNameWithoutExt}" : customPrefix);

                string finalTargetExePath = originalTargetExePath;
                string workingDirectory = folderPath;
                string customIconPath = CustomIconPathTextBox.Text.Trim();
                
                // İkon belirleme: Kullanıcı özel ikon seçtiyse onu kullan, yoksa orijinal exe'yi kullan
                string iconTargetToSet = string.IsNullOrEmpty(customIconPath) ? originalTargetExePath : customIconPath;

                // Son olarak unused vbs path degiskeni kaldirildi / not required
                // 1. Sanal Calistirma Simulasyonu
                if (ChkVirtualRun.IsChecked == true)
                {
                    try
                    {
                        string launchersDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ShortcutManager", "Launchers");
                        Directory.CreateDirectory(launchersDir);

                        string vbsLauncherPath = Path.Combine(launchersDir, $"{shortcutName}_Launcher.vbs");
                        
                        // Root Klasörü Tespiti (Macrium\Reflect\reflect.exe -> Macrium kopyalanacak)
                        string[] pathParts = selectedExeItem.RelativePath.Split('\\');
                        string rootFolderName = pathParts.Length > 1 ? pathParts[0] : "";
                        
                        string sourcePathToCopy = string.IsNullOrEmpty(rootFolderName) ? folderPath : Path.Combine(folderPath, rootFolderName);
                        string relativeExePathInRoot = string.IsNullOrEmpty(rootFolderName) ? selectedExeItem.RelativePath : selectedExeItem.RelativePath.Substring(rootFolderName.Length + 1);

                        // PowerShell scripti oluştur (PS Copy-Item UNC pathlarını natively destekler)
                        string ps1LauncherPath = Path.Combine(launchersDir, $"{shortcutName}_Launcher.ps1");
                        string localAppBase = Path.Combine(
                            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                            "ShortcutManager_VirtualApps",
                            shortcutName);
                        string exeInLocal = Path.Combine(localAppBase, relativeExePathInRoot);

                        var sb = new System.Text.StringBuilder();
                        sb.AppendLine($"$src = '{sourcePathToCopy}'");
                        sb.AppendLine($"$dst = '{localAppBase}'");
                        sb.AppendLine($"$exe = '{exeInLocal}'");
                        sb.AppendLine("if (Test-Path $dst) { Remove-Item $dst -Recurse -Force }");
                        sb.AppendLine("Copy-Item -Path $src -Destination $dst -Recurse -Force");
                        sb.AppendLine("$p = Start-Process -FilePath $exe -PassThru");
                        sb.AppendLine("$p.WaitForExit()");
                        sb.AppendLine("if (Test-Path $dst) { Remove-Item $dst -Recurse -Force }");
                        File.WriteAllText(ps1LauncherPath, sb.ToString(), System.Text.Encoding.UTF8);

                        finalTargetExePath = "powershell.exe";
                        workingDirectory = launchersDir;
                        string psArgs = $"-NoProfile -ExecutionPolicy Bypass -WindowStyle Hidden -File \"{ps1LauncherPath}\"";

                        bool isLauncherCreated = GenerateDesktopShortcutForLauncher(shortcutName, finalTargetExePath, psArgs, workingDirectory, iconTargetToSet);
                        if (isLauncherCreated) successCount++;
                        continue; // Normal kısayol oluşturma adımını atla, VBS oluşturuldu.
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"'{shortcutName}' için sanal başlatıcı oluşturulurken hata: {ex.Message}\nNormal ağ kısayolu oluşturulacak.", "Hata", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                
                // 3. Normal Kısayolu oluştur
                bool isSuccess = GenerateDesktopShortcut(shortcutName, finalTargetExePath, workingDirectory, iconTargetToSet);
                if (isSuccess) successCount++;
            }

            if (successCount > 0)
            {
                MessageBox.Show($"✅ Başarıyla {successCount} adet kısayol masaüstüne bağlandı!", "İşlem Tamam", MessageBoxButton.OK, MessageBoxImage.Information);
                FolderPathTextBox.Clear();
                ExeFilesTreeView.ItemsSource = null;
                _allGroups.Clear();
                ShortcutNameTextBox.Clear();
            }
        }

        private bool GenerateDesktopShortcut(string appName, string targetExePath, string workingDirectory, string iconTargetInfo)
        {
            try
            {
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string shortcutPath = Path.Combine(desktopPath, $"{appName}.lnk");

                // Icon Location her zaman orijinal EXE dosyasını göstermeli (Aksi takdirde C:\VirtualApps içindeki symlink'ten ikonu okuyamayabilir)
                string psCommand = $"$WshShell = New-Object -comObject WScript.Shell; " +
                                   $"$Shortcut = $WshShell.CreateShortcut('{shortcutPath}'); " +
                                   $"$Shortcut.TargetPath = '{targetExePath}'; " +
                                   $"$Shortcut.WorkingDirectory = '{workingDirectory}'; " +
                                   $"$Shortcut.IconLocation = '{iconTargetInfo}, 0'; " +
                                   $"$Shortcut.Save();";

                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = "powershell.exe",
                    Arguments = $"-NoProfile -ExecutionPolicy Bypass -Command \"{psCommand}\"",
                    CreateNoWindow = true,
                    UseShellExecute = false
                };

                Process.Start(psi)?.WaitForExit();
                return File.Exists(shortcutPath);
            }
            catch
            {
                return false;
            }
        }

        private bool GenerateDesktopShortcutForLauncher(string appName, string targetExePath, string arguments, string workingDirectory, string iconTargetInfo)
        {
            try
            {
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string shortcutPath = Path.Combine(desktopPath, $"{appName}.lnk");

                string psCommand = $"$WshShell = New-Object -comObject WScript.Shell; " +
                                   $"$Shortcut = $WshShell.CreateShortcut('{shortcutPath}'); " +
                                   $"$Shortcut.TargetPath = '{targetExePath}'; " +
                                   $"$Shortcut.Arguments = '{arguments}'; " +
                                   $"$Shortcut.WorkingDirectory = '{workingDirectory}'; " +
                                   $"$Shortcut.IconLocation = '{iconTargetInfo}, 0'; " +
                                   $"$Shortcut.Save();";

                ProcessStartInfo psi = new ProcessStartInfo
                {
                    FileName = "powershell.exe",
                    Arguments = $"-NoProfile -ExecutionPolicy Bypass -Command \"{psCommand}\"",
                    CreateNoWindow = true,
                    UseShellExecute = false
                };

                Process.Start(psi)?.WaitForExit();
                return File.Exists(shortcutPath);
            }
            catch
            {
                return false;
            }
        }

        // ==========================================
        // SEKME 2: MEVCUT KISAYOLU (LNK) DÜZENLEME
        // ==========================================
        private void SelectLnkToEdit_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Title = "Düzenlenecek Kısayolu Seçin",
                Filter = "Kısayol Dosyaları (*.lnk)|*.lnk"
            };

            if (dialog.ShowDialog() == true)
            {
                EditLnkPathTextBox.Text = dialog.FileName;
                LoadShortcutDetails(dialog.FileName);
            }
        }

        private void LoadShortcutDetails(string lnkPath)
        {
            try
            {
                Type shellType = Type.GetTypeFromProgID("WScript.Shell");
                dynamic shell = Activator.CreateInstance(shellType);
                var shortcut = shell.CreateShortcut(lnkPath);

                EditTargetPathTextBox.Text = shortcut.TargetPath;
                EditWorkingDirTextBox.Text = shortcut.WorkingDirectory;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Kısayol bilgileri okunamadı: " + ex.Message, "Hata", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateShortcut_Click(object sender, RoutedEventArgs e)
        {
            string lnkPath = EditLnkPathTextBox.Text.Trim();
            string newTargetPath = EditTargetPathTextBox.Text.Trim();
            string newWorkingDir = EditWorkingDirTextBox.Text.Trim();

            if (string.IsNullOrEmpty(lnkPath) || string.IsNullOrEmpty(newTargetPath))
            {
                MessageBox.Show("Lütfen bir kısayol seçin ve yeni Hedef Yol'u doldurun.", "Uyarı", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            try
            {
                Type shellType = Type.GetTypeFromProgID("WScript.Shell");
                dynamic shell = Activator.CreateInstance(shellType);
                var shortcut = shell.CreateShortcut(lnkPath);

                shortcut.TargetPath = newTargetPath;
                shortcut.WorkingDirectory = newWorkingDir;
                shortcut.Save();

                MessageBox.Show("✅ Kısayol başarıyla güncellendi!", "İşlem Tamam", MessageBoxButton.OK, MessageBoxImage.Information);
                EditLnkPathTextBox.Clear();
                EditTargetPathTextBox.Clear();
                EditWorkingDirTextBox.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Hata: " + ex.Message, "Hata", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }

    public class ExeFolderGroup
    {
        public string FolderName { get; set; }
        public System.Collections.Generic.List<ExeFileItem> Items { get; set; } = new();
    }

    public class ExeFileItem
    {
        public string RelativePath { get; set; }
        public string FullPath { get; set; }
        public string FileName { get; set; }
        public string DisplayPath { get; set; }
        public bool IsSelected { get; set; }
    }
}