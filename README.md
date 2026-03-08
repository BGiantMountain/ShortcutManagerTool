<div align="center">

# 🔗 Shortcut Manager Tool

<img src="https://img.shields.io/badge/WPF-Windows--Native-0078D7?style=flat-square&logo=windows" alt="WPF">
<img src="https://img.shields.io/badge/.NET-8.0-512BD4?style=flat-square&logo=dotnet" alt=".NET">
<img src="https://img.shields.io/badge/Version-2.1-22C55E?style=flat-square" alt="v2.1">

A premium, smart, and ultra-lightweight utility for Windows that allows you to generate mass shortcuts, test executables in isolated virtual modes, scan network shares, and easily repair broken shortcut targets. 
<br/>
Windows için toplu kısayol oluşturmanızı, izole sanal modda yürütülebilir dosyaları test etmenizi, ağ paylaşımlarını taramanızı ve bozuk kısayol hedeflerini kolayca onarmanızı sağlayan premium, akıllı ve ultra hafif bir araç.

</div>

---

## 🌍 English

Shortcut Manager is a compact productivity tool designed to manage Windows shortcuts seamlessly. Whether you're configuring a fresh environment or testing standalone applications from a network, Shortcut Manager simplifies the process with its elegant, dark-themed UI and bilingual functionality.

### ✨ Features
- **Bilingual Interface:** Instant language toggle between English and Turkish.
- **Mass Shortcut Creation:** Scan directories up to a specific depth (1-5 levels) and instantly create desktop shortcuts for all underlying `.exe` files simultaneously.
- **Shortcut Scanner (New):** Automatically lists system drives and allows manual entry of unmapped network paths (e.g., `\\Server\Share`) to scan and find all `.lnk` files asynchronously.
- **Folder Shortcuts (Hidden):** Option to set a folder's attribute to "Hidden" and automatically place a shortcut to it in its parent directory.
- **Virtual Run Mode (🔴):** Perfect for network or standalone applications. It temporarily copies the executable into an isolated local folder, runs it, and automatically cleans itself up when the process is closed.
- **Custom Naming & Icons:** Easily assign multiple custom names simultaneously and override default program icons by supplying your own `.ico` or `.exe` file.
- **Shortcut Doctor (Repair):** Select any broken `.lnk` file to quickly inspect and update its target path and working directory.

### 🆕 What's New in v2.1
- **Extreme Size Optimization:** The `.exe` output size has been miraculously shrunk from ~140MB down to **404 KB** by switching to a Framework-Dependent Single-File build format!
- **UI Redesign:** Moved to a modern Grey and Anthracite color palette for a sleek glassmorphic feel. 
- **Dark Theme ComboBox:** Upgraded the Shortcut Scanner's path input to a seamless dark-themed Editable ComboBox.
- **Fixed Bilingual Coverage:** Ensured 100% of all UI labels and identifiers instantly switch between English and Turkish.
- **Status Updates:** Annoying pop-ups removed. Replaced with elegant, temporal green StatusBar messages.
*(Check out [CHANGELOG.md](CHANGELOG.md) for full historical details).*

### 🚀 Usage
1. **Deploying Shortcuts:** Go to the *New Shortcut* tab. Select your base directory. Change shortcut names inline. Click "Create Desktop Shortcut".
2. **Scanning Networks:** Go to *Shortcut Scanner* tab. Enter a path like `\\Shared\Apps` and hit Scan.
3. **Fixing Existing Shortcuts:** Go to the *Fix Shortcut* tab. Select the `.lnk` file. Update its Target and Working Directory easily.

---

## 🇹🇷 Türkçe

Shortcut Manager, Windows kısayollarını sorunsuz bir şekilde yönetmek için tasarlanmış kompakt bir üretkenlik aracıdır. Yeni bir sistem yapılandırırken veya portable uygulamaları ağ üzerinden kurarken zarif, antrasit temalı çift dilli arayüzü ile süreci uçtan uca kolaylaştırır.

### ✨ Özellikler
- **Çift Dil Desteği:** İngilizce ve Türkçe arasında tek tuşla anında arayüz değişimi.
- **Toplu Kısayol Oluşturma:** Seçtiğiniz bir klasörü (1-5 alt seviyeye) kadar tarayarak içindeki `.exe` dosyaları için aynı anda devasa masaüstü kısayolları oluşturun.
- **Kısayol Tarayıcısı (Yeni):** Sistem disklerini otomatik bulur ve ekstra olarak manuel ağ yollarını (`\\Sunucu\Paylasim` vb.) yazıp asenkron olarak içindeki `.lnk` dosyalarını taratmanızı sağlar.
- **Klasör Kısayolları (Gizli):** Hedef klasörü tek tuşla "Gizli" yapıp bir üst dizinine otomatik kısayolunu yerleştirir.
- **Sanal Mod (🔴):** Ağ programları için kusursuzdur. Çalıştırılabilir dosyayı yerel, izole bir klasöre kopyalar, çalıştırır ve program kapatıldığında otomatik olarak kendisini siler.
- **Özel İsimlendirme & İkonlar:** Listeden her bir programa özel anında TextBox ile farklı isimler yazın ve isterseniz kendi `.ico` veya farklı hedefin `.exe` simgesini şablon olarak kullanın.
- **Kısayol Onarımı (Repair):** Bozulmuş bilgisayar `.lnk` kısayol dosyalarını seçerek Hedef Klasör (Target) ve Başlangıç Yeri değerlerini tek tuşla anında güncelleyin.

### 🆕 v2.1'de Neler Yeni?
- **Ultra Boyut Optimizasyonu:** Uygulamanın final derlenmiş dosya ağırlığı Framework-Dependent (Sisteme Bağımlı) Single-File sistemiyle ~140 MB bandından **404 KB (0.4 MB)** seviyesine indirgendi!
- **Premium UI Kullanımı:** Bütün arayüz mavi ağırlıklı tasarımdan, profesyonel "Gri ve Antrasit" cam efektli yapıya taşındı. Kısayol Tarayıcısındaki arama barı %100 Dark Mode uyumlu hale getirildi.
- **Çeviri Kapsamı Genişletildi:** İngilizce/Türkçe dil motoru tüm alt etiketleri (Başlama Yeri, Derinlik vb.) kapsayacak şekilde %100'e çıkarıldı ve "Shortcut Manager" adı global olarak sabitlendi.
- **Durum Çubuğu Güncellemesi:** İşlem sonrası çıkan kutucuklar (MessageBox) kaldırılıp yerine StatusBar üzerine 5 saniyelik zarif yeşil başarılı bildirimleri eklendi.
*(Bütün geçmişini okumak için [CHANGELOG.md](CHANGELOG.md) dosyasına göz atın).*

### 🚀 Kullanım
1. **Kısayol Dağıtımı:** *Yeni Kısayol* sekmesine gidin. Kaynak klasörü seçin. Listeden istediğiniz isimleri TextBox ile düzenleyin ve oluşturun.
2. **Ağ Tarama:** *Kısayol Tarayıcısı* sekmesine geçip üst alana `\\AğDiski\Ortak` yazın ve Ara butonuna basın.
3. **Mevcut Kısayolları Onarma:** *Kısayol Onar* sekmesine gidin. Bozuk olan `.lnk` veya sistem kısayolunu seçip yeni yolunu güncelleyip kaydedin.

---

### 📋 Build / Derleme

**Shortcut Manager v2.1** requires **.NET 8.0 Desktop Runtime**.
You can compile it into an ultra-small 400KB single executable using the terminal command below:
<br/>
Bu proje **.NET 8.0 Masaüstü Çalışma Zamanı** gerektirir. Uygulamayı 404 KB'lık tek bir `.exe` halinde derlemek için aşağıdaki MSBuild komutunu kullanabilirsiniz:

```bash
dotnet publish "ShortcutManager.csproj" -c Release -r win-x64 --self-contained false -p:PublishSingleFile=true
```
