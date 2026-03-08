using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Collections.Generic;
using MessageBox = System.Windows.MessageBox;
using OpenFileDialog = Microsoft.Win32.OpenFileDialog;
using OpenFolderDialog = Microsoft.Win32.OpenFolderDialog;
using Color = System.Windows.Media.Color;
using ColorConverter = System.Windows.Media.ColorConverter;
using Microsoft.Win32;
using Path = System.IO.Path;

namespace ShortcutManager
{
    public partial class MainWindow : Window, INotifyPropertyChanged
    {
        private bool _isEnglish = false;
        private DispatcherTimer _statusTimer;

        public event PropertyChangedEventHandler? PropertyChanged;

        private ObservableCollection<DriveInfo> _systemDrives;
        public ObservableCollection<DriveInfo> SystemDrives
        {
            get => _systemDrives;
            set
            {
                _systemDrives = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(SystemDrives)));
            }
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = this;

            // Load drives
            try {
                SystemDrives = new ObservableCollection<DriveInfo>(DriveInfo.GetDrives().Where(d => d.IsReady));
                if (SystemDrives.Count > 0)
                    DrivesComboBox.SelectedIndex = 0;
            } catch { }

            // Timer for status bar
            _statusTimer = new DispatcherTimer { Interval = TimeSpan.FromSeconds(5) };
            _statusTimer.Tick += StatusTimer_Tick;

            // Detect OS language and set default
            if (System.Globalization.CultureInfo.CurrentUICulture.TwoLetterISOLanguageName.Equals("en", StringComparison.OrdinalIgnoreCase))
            {
                _isEnglish = true;
            }
            ApplyLanguage();
        }

        private void StatusTimer_Tick(object? sender, EventArgs e)
        {
            _statusTimer.Stop();
            StatusLabel.Text = _isEnglish ? "Ready" : "Hazır";
            StatusLabel.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#475569"));
        }

        private void ShowSuccessStatus(string msg)
        {
            StatusLabel.Text = msg;
            StatusLabel.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#10B981")); // Success Green
            _statusTimer.Stop();
            _statusTimer.Start();
        }

        // ==========================================
        // DİL YÖNETİMİ (LOCALIZATION)
        // ==========================================
        private void LangToggle_Click(object sender, RoutedEventArgs e)
        {
            _isEnglish = !_isEnglish;
            ApplyLanguage();
        }

        private void ApplyLanguage()
        {
            LangBtn.Content = _isEnglish ? "🇹🇷 TR" : "🇬🇧 EN";
            MainWindowEl.Title = "Shortcut Manager";
            
            HeaderTitleTxt.Text = "Shortcut Manager";
            HeaderSubtitleTxt.Text = _isEnglish ? "Create · Repair · Virtual Run Shortcuts" : "Kısayol oluşturun · Onarın · Sanal modda çalıştırın";
            
            TabNewShortcut.Header = _isEnglish ? "New Shortcut" : "Yeni Kısayol";
            TabFixShortcut.Header = _isEnglish ? "Fix Shortcut" : "Kısayol Onar";
            TabScanner.Header = _isEnglish ? "Shortcut Scanner" : "Kısayol Tarayıcısı";
            ScannerDescTxt.Text = _isEnglish ? "System Drives or Network Path (e.g. \\\\Server\\Share):" : "Sistem Diskleri veya Ağ Yolu (örn: \\\\Sunucu\\Paylasim):";

            BtnBrowseFolder.Content = _isEnglish ? "📂 Select Folder" : "📂 Klasör Seç";
            ChkCreateFolderShortcut.Content = _isEnglish ? "📁 Make Folder Shortcut (Hide)" : "📁 Klasörü Kısayol Yap (Gizle)";
            BtnBrowseIcon.Content = _isEnglish ? "🎨 Select Icon" : "🎨 İkon Seç";
            BtnCreateDesktopShortcut.Content = _isEnglish ? "🚀 Create Desktop Shortcut" : "🚀 Masaüstüne Kısayol Oluştur";
            
            BtnSelectShortcut.Content = _isEnglish ? "📂 Select Shortcut" : "📂 Kısayol Seç";
            BtnSaveFix.Content = _isEnglish ? "💾 Save Changes" : "💾 Değişiklikleri Kaydet";
            
            ScanBtn.Content = _isEnglish ? "Scan" : "Ara";
            
            // Missing UI Labels
            Step1Txt.Text = _isEnglish ? "01. APPLICATION FOLDER" : "01. UYGULAMA KLASÖRÜ";
            DepthTxt.Text = _isEnglish ? "Depth" : "Derinlik";
            SearchLblTxt.Text = _isEnglish ? "🔍 Search:" : "🔍 Arama:";
            
            ChkVirtualRun.Content = _isEnglish ? "🔴 Virtual Run" : "🔴 Sanal Mod";
            
            StepFix1Txt.Text = _isEnglish ? "01. SHORTCUT TO EDIT (.lnk)" : "01. DÜZENLENECEK KISAYOL (.lnk)";
            StepFix2Txt.Text = _isEnglish ? "02. NEW VALUES" : "02. YENİ DEĞERLER";
            
            LblTargetTxt.Text = _isEnglish ? "Target Path" : "Hedef Yol (Target Path)";
            LblWorkingTxt.Text = _isEnglish ? "Working Directory" : "Başlama Yeri (Working Directory)";
            
            if (!_statusTimer.IsEnabled)
                StatusLabel.Text = _isEnglish ? "Ready" : "Hazır";
        }

        // ==========================================
        // SEKME 1: SIFIRDAN KISAYOL OLUŞTURMA (EXE)
        // ==========================================
        private void BrowseFolder_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFolderDialog { Title = _isEnglish ? "Select Application Folder" : "Uygulama Klasörünü Seçin" };
            if (dialog.ShowDialog() == true)
            {
                FolderPathTextBox.Text = dialog.FolderName;
            }
        }

        private void BrowseIcon_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog
            {
                Title = _isEnglish ? "Select Custom Icon (.ico or .exe)" : "Özel Kısayol İkonunu Seçin (.ico veya .exe)",
                Filter = "Icons/Exe (*.ico;*.exe)|*.ico;*.exe"
            };

            if (dialog.ShowDialog() == true)
            {
                CustomIconPathTextBox.Text = dialog.FileName;
            }
        }

        private List<ExeFolderGroup> _allGroups = new();

        private void FolderPathTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string path = FolderPathTextBox.Text.Trim();
            if (Directory.Exists(path))
            {
                int depth = (int)(DepthSlider?.Value ?? 3);
                LoadExeFilesInFolder(path, depth);
            }
            else
            {
                _allGroups.Clear();
                ExeFilesTreeView.ItemsSource = null;
            }
        }

        private void DepthSlider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            string path = FolderPathTextBox?.Text?.Trim();
            if (!string.IsNullOrEmpty(path) && Directory.Exists(path))
            {
                LoadExeFilesInFolder(path, (int)e.NewValue);
            }
        }

        private void SearchTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ApplySearchFilter(SearchTextBox.Text.Trim().ToLowerInvariant());
        }

        private void ApplySearchFilter(string query)
        {
            if (_allGroups == null) return;
            if (string.IsNullOrEmpty(query))
            {
                ExeFilesTreeView.ItemsSource = _allGroups;
                return;
            }
            var filtered = new List<ExeFolderGroup>();
            foreach (var group in _allGroups)
            {
                var matchingItems = group.Items.Where(i => i.FileName.ToLowerInvariant().Contains(query) || group.FolderName.ToLowerInvariant().Contains(query)).ToList();
                if (matchingItems.Count > 0)
                    filtered.Add(new ExeFolderGroup { FolderName = group.FolderName, Items = new ObservableCollection<ExeFileItem>(matchingItems) });
            }
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
                var groups = new Dictionary<string, ExeFolderGroup>();

                foreach (string exe in exeFiles)
                {
                    string relativePath = exe.Substring(folderPath.Length).TrimStart('\\');
                    string[] parts = relativePath.Split('\\');
                    string folderKey = parts.Length > 1 ? string.Join("\\", parts, 0, parts.Length - 1) : (_isEnglish ? "Root Folder" : "Ön Klasör");

                    if (!groups.ContainsKey(folderKey))
                    {
                        string displayName = parts.Length > 1 ? parts[0] : (_isEnglish ? "Root Folder" : "Ön Klasör");
                        if (parts.Length > 2) displayName = folderKey;
                        groups[folderKey] = new ExeFolderGroup { FolderName = displayName };
                    }

                    string exeName = Path.GetFileNameWithoutExtension(exe);
                    groups[folderKey].Items.Add(new ExeFileItem
                    {
                        FullPath = exe,
                        RelativePath = relativePath,
                        FileName = Path.GetFileName(exe),
                        CustomName = exeName,
                        IsSelected = false
                    });
                }

                _allGroups = groups.Values.ToList();
                if (_allGroups.Count == 1 && _allGroups[0].Items.Count > 0)
                    _allGroups[0].Items[0].IsSelected = true;

                ExeFilesTreeView.ItemsSource = _allGroups;

                foreach (var item in ExeFilesTreeView.Items)
                {
                    if (ExeFilesTreeView.ItemContainerGenerator.ContainerFromItem(item) is TreeViewItem tvi)
                        tvi.IsExpanded = true;
                }
            }
            catch { }
        }

        private void CreateShortcut_Click(object sender, RoutedEventArgs e)
        {
            string folderPath = FolderPathTextBox.Text.Trim();
            if (string.IsNullOrEmpty(folderPath) || !Directory.Exists(folderPath))
            {
                MessageBox.Show(_isEnglish ? "Please select a valid folder." : "Łütfen geçerli bir klasör yolu girin.", "Uyarı", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            int successCount = 0;

            // FOLDER SHORTCUT AND HIDE LOGIC
            if (ChkCreateFolderShortcut.IsChecked == true)
            {
                try
                {
                    var dirInfo = new DirectoryInfo(folderPath);
                    dirInfo.Attributes |= FileAttributes.Hidden;

                    string parentDir = dirInfo.Parent?.FullName ?? Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                    string shortcutPath = Path.Combine(parentDir, $"{dirInfo.Name}.lnk");
                    
                    if (GenerateShortcut(shortcutPath, folderPath, parentDir, ""))
                        successCount++;
                }
                catch (Exception ex)
                {
                    MessageBox.Show((_isEnglish ? "Folder hide/shortcut error: " : "Klasör kısayol/gizleme hatası: ") + ex.Message, "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }

            // EXE SHORTCUT LOGIC
            var selectedItems = new List<ExeFileItem>();
            if (ExeFilesTreeView.ItemsSource is IEnumerable<ExeFolderGroup> groups)
            {
                foreach (var group in groups)
                    foreach (var item in group.Items)
                        if (item.IsSelected) selectedItems.Add(item);
            }

            if (selectedItems.Count == 0 && ChkCreateFolderShortcut.IsChecked != true)
            {
                MessageBox.Show(_isEnglish ? "Please select at least one executable (.exe)." : "Lütfen listeden en az bir çalıştırılabilir dosya (.exe) seçin.", "Uyarı", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            foreach (var selectedExeItem in selectedItems)
            {
                string originalTargetExePath = selectedExeItem.FullPath;
                string shortcutName = string.IsNullOrWhiteSpace(selectedExeItem.CustomName) ? selectedExeItem.FileName : selectedExeItem.CustomName;

                string finalTargetExePath = originalTargetExePath;
                string workingDirectory = folderPath;
                string customIconPath = CustomIconPathTextBox.Text.Trim();
                string iconTargetToSet = string.IsNullOrEmpty(customIconPath) ? originalTargetExePath : customIconPath;

                if (ChkVirtualRun.IsChecked == true)
                {
                    try
                    {
                        string launchersDir = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "ShortcutManager", "Launchers");
                        Directory.CreateDirectory(launchersDir);

                        string[] pathParts = selectedExeItem.RelativePath.Split('\\');
                        string rootFolderName = pathParts.Length > 1 ? pathParts[0] : "";
                        
                        string sourcePathToCopy = string.IsNullOrEmpty(rootFolderName) ? folderPath : Path.Combine(folderPath, rootFolderName);
                        string relativeExePathInRoot = string.IsNullOrEmpty(rootFolderName) ? selectedExeItem.RelativePath : selectedExeItem.RelativePath.Substring(rootFolderName.Length + 1);

                        string ps1LauncherPath = Path.Combine(launchersDir, $"{shortcutName}_Launcher.ps1");
                        string localAppBase = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "ShortcutManager_VirtualApps", shortcutName);
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

                        string destShortcutPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), $"{shortcutName}.lnk");
                        
                        if (GenerateShortcutLauncher(destShortcutPath, finalTargetExePath, psArgs, workingDirectory, iconTargetToSet))
                            successCount++;
                        continue;
                    }
                    catch { }
                }

                string stdDestShortcutPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), $"{shortcutName}.lnk");
                if (GenerateShortcut(stdDestShortcutPath, finalTargetExePath, workingDirectory, iconTargetToSet))
                    successCount++;
            }

            if (successCount > 0)
            {
                ShowSuccessStatus(_isEnglish ? $"✅ {successCount} shortcuts connected." : $"✅ {successCount} kısayol başarıyla bağlandı.");
                FolderPathTextBox.Clear();
                ExeFilesTreeView.ItemsSource = null;
                _allGroups.Clear();
            }
        }

        private bool GenerateShortcut(string shortcutPath, string targetExePath, string workingDirectory, string iconTargetInfo)
        {
            try
            {
                string psCommand = $"$WshShell = New-Object -comObject WScript.Shell; $Shortcut = $WshShell.CreateShortcut('{shortcutPath}'); $Shortcut.TargetPath = '{targetExePath}'; $Shortcut.WorkingDirectory = '{workingDirectory}'; $Shortcut.IconLocation = '{iconTargetInfo}, 0'; $Shortcut.Save();";
                Process.Start(new ProcessStartInfo { FileName = "powershell.exe", Arguments = $"-NoProfile -ExecutionPolicy Bypass -Command \"{psCommand}\"", CreateNoWindow = true, UseShellExecute = false })?.WaitForExit();
                return File.Exists(shortcutPath);
            }
            catch { return false; }
        }

        private bool GenerateShortcutLauncher(string shortcutPath, string targetExePath, string arguments, string workingDirectory, string iconTargetInfo)
        {
            try
            {
                string psCommand = $"$WshShell = New-Object -comObject WScript.Shell; $Shortcut = $WshShell.CreateShortcut('{shortcutPath}'); $Shortcut.TargetPath = '{targetExePath}'; $Shortcut.Arguments = '{arguments}'; $Shortcut.WorkingDirectory = '{workingDirectory}'; $Shortcut.IconLocation = '{iconTargetInfo}, 0'; $Shortcut.Save();";
                Process.Start(new ProcessStartInfo { FileName = "powershell.exe", Arguments = $"-NoProfile -ExecutionPolicy Bypass -Command \"{psCommand}\"", CreateNoWindow = true, UseShellExecute = false })?.WaitForExit();
                return File.Exists(shortcutPath);
            }
            catch { return false; }
        }

        // ==========================================
        // SEKME 2: MEVCUT KISAYOLU (LNK) DÜZENLEME
        // ==========================================
        private void SelectLnkToEdit_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new OpenFileDialog { Title = _isEnglish ? "Select Shortcut" : "Düzenlenecek Kısayolu Seçin", Filter = "Kısayol Dosyaları (*.lnk)|*.lnk" };
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
                MessageBox.Show("Read Error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void UpdateShortcut_Click(object sender, RoutedEventArgs e)
        {
            string lnkPath = EditLnkPathTextBox.Text.Trim();
            string newTargetPath = EditTargetPathTextBox.Text.Trim();
            if (string.IsNullOrEmpty(lnkPath) || string.IsNullOrEmpty(newTargetPath)) return;

            try
            {
                Type shellType = Type.GetTypeFromProgID("WScript.Shell");
                dynamic shell = Activator.CreateInstance(shellType);
                var shortcut = shell.CreateShortcut(lnkPath);
                shortcut.TargetPath = newTargetPath;
                shortcut.WorkingDirectory = EditWorkingDirTextBox.Text.Trim();
                shortcut.Save();
                ShowSuccessStatus(_isEnglish ? "✅ Shortcut updated!" : "✅ Kısayol başarıyla güncellendi!");
                EditLnkPathTextBox.Clear();
                EditTargetPathTextBox.Clear();
                EditWorkingDirTextBox.Clear();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        // ==========================================
        // SEKME 3: KISAYOL TARAYICISI
        // ==========================================
        private void ScanShortcuts_Click(object sender, RoutedEventArgs e)
        {
            string rootPath = DrivesComboBox.Text?.Trim() ?? string.Empty;

            if (!string.IsNullOrEmpty(rootPath))
            {
                if (!Directory.Exists(rootPath))
                {
                    MessageBox.Show(_isEnglish ? "Path cannot be accessed or does not exist." : "Belirtilen yol bulunamadı veya erişilemiyor.", "Uyarı", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                StatusLabel.Text = _isEnglish ? "Scanning..." : "Taranıyor...";
                ScannedShortcutsList.ItemsSource = null;
                
                // Fire async scan to not block UI
                System.Threading.Tasks.Task.Run(() =>
                {
                    var results = new ObservableCollection<ShortcutItem>();
                    try
                    {
                        var options = new EnumerationOptions { IgnoreInaccessible = true, RecurseSubdirectories = true };
                        var files = Directory.EnumerateFiles(rootPath, "*.lnk", options).Take(200); // Limit 200 to prevent ultra slow perf UI
                        Type shellType = Type.GetTypeFromProgID("WScript.Shell");
                        
                        foreach (var f in files)
                        {
                            try {
                                dynamic shell = Activator.CreateInstance(shellType);
                                var sc = shell.CreateShortcut(f);
                                string target = sc.TargetPath;
                                if (!string.IsNullOrEmpty(target))
                                    results.Add(new ShortcutItem { Name = Path.GetFileName(f), TargetPath = target, ShortcutPath = f });
                            } catch { }
                        }
                    }
                    catch { }

                    Dispatcher.Invoke(() =>
                    {
                        ScannedShortcutsList.ItemsSource = results;
                        ShowSuccessStatus(_isEnglish ? $"✅ {results.Count} found in {rootPath}." : $"✅ {results.Count} adet {rootPath} içinde bulundu.");
                    });
                });
            }
        }
    }

    public class ExeFolderGroup
    {
        public string FolderName { get; set; }
        public ObservableCollection<ExeFileItem> Items { get; set; } = new();
    }

    public class ExeFileItem : INotifyPropertyChanged
    {
        public string RelativePath { get; set; }
        public string FullPath { get; set; }
        public string FileName { get; set; }
        
        private string _customName;
        public string CustomName 
        { 
            get => _customName; 
            set { _customName = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(CustomName))); } 
        }
        
        private bool _isSelected;
        public bool IsSelected 
        { 
            get => _isSelected; 
            set { _isSelected = value; PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(IsSelected))); } 
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }

    public class ShortcutItem
    {
        public string Name { get; set; }
        public string TargetPath { get; set; }
        public string ShortcutPath { get; set; }
    }
}
