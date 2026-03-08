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
- **Premium UI:** Refined dark and glassmorphism-inspired UI for maximum readability and visual appeal.

### 🚀 Usage

1. **Deploying Shortcuts & Virtual Mode:** 
   Go to the *New Shortcut* tab. Select your base directory and adjust the search depth. Select the executable(s) you wish to create a shortcut for. Check "Virtual Mode" if you want the app to run in a temporary container without leaving traces behind. Click "Create Desktop Shortcut".
2. **Fixing Existing Shortcuts:**
   Go to the *Repair Shortcut* tab. Select the `.lnk` file you want to fix. Directly update its Target and Working Directory without wrestling with native Windows properties dialogs.

---

## 🇹🇷 Türkçe

Akıllı Kısayol Merkezi, Windows kısayollarını sorunsuz bir şekilde yönetmek için tasarlanmış kompakt bir üretkenlik aracıdır. Yeni bir sistem yapılandırırken veya ağ üzerindeki bağımsız uygulamaları (portable/standalone) test ederken, zarif ve koyu temalı arayüzü ile süreci büyük oranda basitleştirir.

### ✨ Özellikler

- **Toplu Kısayol Oluşturma:** Seçtiğiniz bir klasörü belirlediğiniz derinlik seviyesine (1-5 seviye) kadar tarayarak içindeki `.exe` dosyaları için aynı anda masaüstü kısayolları oluşturun.
- **Akıllı Filtreleme:** Büyük klasörlerdeki yüzlerce dosya arasında anında arama yaparak sadece istediğiniz uygulamayı bulun.
- **Sanal Mod (🔴):** Ağ programları veya kurulumsulaştırılmış (portable) uygulamalar için kusursuzdur. Çalıştırılabilir dosyayı geçici ve izole bir yerel klasöre kopyalar, çalıştırır ve program kapatıldığında hiçbir iz bırakmadan otomatik olarak kendini tamamen siler.
- **Özel İkonlar:** Kısayolları oluştururken kendi `.ico` veya hedef `.exe` dosyanızı göstererek orijinal uygulama ikonlarını değiştirebilirsiniz.
- **Kısayol Onarımı:** Hedefi değiştirilmiş veya bozulmuş herhangi bir `.lnk` dosyasını seçerek Hedef Klasör (Target) ve Başlangıç Yeri (Working Directory) değerlerini anında onarın.
- **Premium Arayüz:** Maksimum okunabilirlik ve şıklık için cam benzeri (glassmorphism) tasarımdan ilham alan modern, tam karanlık mod arayüz.

### 🚀 Kullanım

1. **Kısayol Dağıtımı & Sanal Çalıştırma:**
   *Yeni Kısayol* sekmesine gidin. Kaynak klasörü seçin ve arama derinliğini ayarlayın. Listelenen `.exe`'lerden istediklerinizi seçin. Uygulamanın kalıntı bırakmadan izole bir şekilde çalışmasını isterseniz "Sanal Mod"u işaretleyin. "Masaüstüne Kısayol Oluştur" butonuna tıklayarak işlemi tamamlayın.
2. **Mevcut Kısayolları Onarma:**
   *Kısayol Onar* sekmesine gidin. Bozuk olan `.lnk` dosyasını seçin. Klasik Windows özellikler penceresiyle boğuşmadan Hedef ve Başlama yerini direkt güncelleyip kaydedin.

---

### 📋 Build / Derleme

This project requires **.NET 8.0**. You can compile it into a single, standalone executable using the terminal command below:
<br/>
Bu proje **.NET 8.0** gerektirir. Uygulamayı kendi başına çalışabilen tek bir `.exe` (standalone) halinde paketlemek için aşağıdaki komutu kullanabilirsiniz:

```bash
dotnet publish "ShortcutManager.csproj" -c Release -r win-x64 --self-contained true -p:PublishSingleFile=true -p:IncludeNativeLibrariesForSelfExtract=true -o "bin\Publish"
```
