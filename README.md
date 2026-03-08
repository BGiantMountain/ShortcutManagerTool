<div align="center">

# 🔗 Shortcut Manager Tool / Akıllı Kısayol Merkezi

<img src="https://img.shields.io/badge/WPF-Windows--Native-0078D7?style=flat-square&logo=windows" alt="WPF">
<img src="https://img.shields.io/badge/.NET-8.0-512BD4?style=flat-square&logo=dotnet" alt=".NET">
<img src="https://img.shields.io/badge/Version-2.0-22C55E?style=flat-square" alt="v2.0">

A premium, smart, and lightweight utility for Windows that allows you to generate mass shortcuts, test executables in isolated virtual modes, and easily repair broken shortcut targets. 
<br/>
Windows için toplu kısayol oluşturmanızı, izole sanal modda yürütülebilir dosyaları test etmenizi ve bozuk kısayol hedeflerini kolayca onarmanızı sağlayan premium, akıllı ve hafif bir araç.

</div>

---

## 🌍 English

Shortcut Manager is a compact productivity tool designed to manage Windows shortcuts seamlessly. Whether you're configuring a fresh environment or testing standalone applications from a network, Shortcut Manager simplifies the process with its elegant, dark-themed UI.

### ✨ Features
- **Mass Shortcut Creation:** Scan directories up to a specific depth (1-5 levels) and instantly create desktop shortcuts for all underlying `.exe` files simultaneously.
- **Smart Filtering:** Quickly search and filter specific applications within large folders.
- **Virtual Run Mode (🔴):** Perfect for network or standalone applications. It temporarily copies the executable into an isolated local folder, runs it, and automatically cleans itself up when the process is closed.
- **Custom Icons:** Easily override default program icons by supplying your own `.ico` or `.exe` file.
- **Shortcut Doctor (Repair):** Select any broken `.lnk` file to quickly inspect and update its target path and working directory.

### 🆕 What's New in v2.0 (Latest Release)
- **Extreme UI Redesign:** Replaced the old standard flat look with a highly polished, deep dark theme featuring glassmorphic cards, gradient headers, and button glowing effects.
- **Size Optimization:** The `.exe` output size has been highly reduced by ~55% (from 162MB down to ~72MB) using Single-File Compression.
- **Form & Layout Polishes:** Completely centered text typing zones, resolved text clipping issues, and arranged the complex "Virtual Mode" settings into a wide, breathable modular grid.
*(Check out [CHANGELOG.md](CHANGELOG.md) for full historical details).*

### 🚀 Usage
1. **Deploying Shortcuts:** Go to the *New Shortcut* tab. Select your base directory and adjust the search depth. Check "Virtual Mode" to sandbox the application tracking. Click "Create Desktop Shortcut".
2. **Fixing Existing Shortcuts:** Go to the *Repair Shortcut* tab. Select the `.lnk` file. Update its Target and Working Directory easily.

---

## 🇹🇷 Türkçe

Akıllı Kısayol Merkezi, Windows kısayollarını sorunsuz bir şekilde yönetmek için tasarlanmış kompakt bir üretkenlik aracıdır. Yeni bir sistem yapılandırırken veya portable uygulamaları ağ üzerinden kurarken zarif ve koyu temalı arayüzü ile süreci uçtan uca yönetir.

### ✨ Özellikler
- **Toplu Kısayol Oluşturma:** Seçtiğiniz bir klasörü (1-5 alt seviyeye) kadar tarayarak içindeki `.exe` dosyaları için aynı anda devasa masaüstü kısayolları oluşturun.
- **Akıllı Filtreleme:** Büyük klasörlerde anında `.exe` araması yaparak sadece hedeflenen programı listede süzün.
- **Sanal Mod (🔴):** Ağ programları veya kurulumsulaştırılmış uygulamalar için kusursuzdur. Çalıştırılabilir dosyayı yerel, izole bir klasöre kopyalar, çalıştırır ve program kapatıldığında otomatik olarak kendisini siler.
- **Özel İkonlar:** Kısayolları oluştururken kendi `.ico` veya farklı bir hedef `.exe` simgenizi şablon olarak kullanabilme.
- **Kısayol Onarımı (Repair):** Bozulmuş bilgisayar `.lnk` kısayol dosyalarını seçerek Hedef Klasör (Target) ve Başlangıç Yeri değerlerini tek tuşla anında doğrulayıp güncelleyin.

### 🆕 v2.0'da Neler Yeni? (Son Güncelleme)
- **Kapsamlı UI Tasarımı (Premium Update):** Arayüz cam efekti hisli koyu "Card Box" tasarım diline taşındı. Butonlarda dinamik etkileşim (Parlama / Glow) animasyonları, çok daha okunabilir padding/hizalama özellikleri devreye sokuldu.
- **Boyut (Exe) Optimizasyonu:** Uygulamanın final derlenmiş dosya ağırlığı Single-File Compression komutu eklenerek ~162 MB boyutundan **%55** kâr ile ~71.9 MB bandına indirgendi ve performans uçuruldu.
- **Oluşturma Kutularının Rahatlatılması:** Sıkışık olan metin kutuları genişletildi. İçerisindeki metin okuma hataları `VerticalContentAlignment` ile giderildi ve "Sanal Mod" arayüzü temiz bir sütun eşleşmesi içine yerleştirildi.
*(Bütün süreç geçmişini okumak için lütfen [CHANGELOG.md](CHANGELOG.md) dosyasına göz atın).*

### 🚀 Kullanım
1. **Kısayol Dağıtımı & Sanal Çalıştırma:** *Yeni Kısayol* sekmesine gidin. Kaynak klasörü seçin. Listelenen `.exe`'lerden istediklerinizi seçin. Uygulamanın kalıntı bırakmadan izole bir şekilde çalışmasını isterseniz "Sanal Mod"u işaretleyin ve Oluştur tuşuna basın.
2. **Mevcut Kısayolları Onarma:** *Kısayol Onar* sekmesine gidin. Bozuk olan `.lnk` (Kısayol) dosyasını seçin. Klasik Windows özellikler penceresiyle yorulmadan direkt yeni bir yola bağlayıp kaydedin.

---

### 📋 Build / Derleme

This project requires **.NET 8.0**. You can compile it into a single, standalone executable using the terminal command below:
<br/>
Bu proje **.NET 8.0** gerektirir. Uygulamayı kendi başına çalışabilen tek bir `.exe` (standalone) halinde paketlemek için terminalde dizine gidip aşağıdaki komutu kullanabilirsiniz:

```bash
dotnet publish "ShortcutManager.csproj" -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -p:EnableCompressionInSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true -o "bin\PublishCompressed"
```
