# FlappyBird C# Oyunu

Bu proje, C# WinForms kullanılarak geliştirilmiş modern bir FlappyBird oyunudur.

## 🎮 Oyun Özellikleri

### 🎯 Ortak Özellikler (Her İki Versiyon)
- **Kırmızı Kuş**: Gerçekçi kanat çırpma animasyonu ile
- **3D Borular**: Gerçekçi metal boru görünümü, gölgeler ve detaylar
- **Dinamik Temalar**: 4 farklı arka plan teması
  - 🌅 Gündüz
  - 🌇 Günbatımı  
  - 🌙 Gece (yıldızlı)
  - 🌄 Şafak
- **Değişken Zorluk**: Rastgele yükseklikte borular
- **Skor Sistemi**: Her boru geçişinde skor artışı

### 🖥️ Windows Versiyonu Özel Özellikler
- **Yüksek Performans**: Native Windows uygulaması
- **Tam Ekran**: Tam ekran modu desteği
- **Gelişmiş Grafik**: GDI+ ile yüksek kalite çizim
- **P Tuşu**: Oyunu durdur/devam ettir

### 🌐 Web Versiyonu Özel Özellikler
- **Cross-Platform**: Herhangi bir işletim sisteminde çalışır
- **Tarayıcı Uyumlu**: Chrome, Firefox, Safari, Edge
- **Responsive**: Farklı ekran boyutlarında uyum
- **GitHub Codespaces**: Bulut ortamında çalışır

## 🎯 Kontroller

### 🖥️ Windows Versiyonu
- **Boşluk Tuşu**: Kuşu zıplat
- **P Tuşu**: Oyunu durdur/devam ettir
- **Oyun Bittiğinde Boşluk**: Yeniden başlat

### 🌐 Web Versiyonu
- **Boşluk Tuşu**: Kuşu zıplat
- **Oyun Bittiğinde**: "Yeniden Başla" butonuna tıklayın

## 🚀 Çalıştırma

Bu proje **iki farklı versiyon** içerir:

### 🖥️ Versiyon 1: Windows Desktop (.NET WinForms)
**Özellikler**: Tam WinForms, 3D borular, kanat animasyonu, yüksek performans
**Platform**: Sadece Windows
**Gereksinimler**: .NET 8.0 SDK, Windows işletim sistemi

#### Yöntem 1: Visual Studio (En Kolay)
1. **File** → **Open** → **Project/Solution**
2. `FlappyBirdGame.csproj` dosyasını açın
3. **F5** tuşuna basın

#### Yöntem 2: Terminal
```bash
# Projeyi klonlayın
git clone https://github.com/CDenizSahin/flappybird-charp.git
cd flappybird-charp

# Windows versiyonunu çalıştırın
dotnet run --project FlappyBirdGame.csproj
```

### 🌐 Versiyon 2: Web Blazor (Cross-Platform)
**Özellikler**: Tarayıcıda çalışır, cross-platform, GitHub Codespaces uyumlu
**Platform**: Linux, Windows, macOS, herhangi bir tarayıcı
**Gereksinimler**: .NET 8.0 SDK

#### Yöntem 1: GitHub Codespaces (Önerilen)
1. Bu repository'yi fork edin
2. **Code** → **Codespaces** → **Create codespace**
3. Terminal'de:
```bash
dotnet run --project FlappyBirdWeb.csproj
```
4. Tarayıcıda oyun açılır

#### Yöntem 2: Yerel Web Versiyonu
```bash
# Projeyi klonlayın
git clone https://github.com/CDenizSahin/flappybird-charp.git
cd flappybird-charp

# Web versiyonunu çalıştırın
dotnet run --project FlappyBirdWeb.csproj
```

### 📁 Proje Yapısı
```
FlappyBirdGame/
├── FlappyBirdGame.csproj     # Windows WinForms versiyonu
├── FlappyBirdWeb.csproj      # Web Blazor versiyonu
├── Form1.cs                  # Windows oyun kodu
├── Pages/Index.razor        # Web oyun kodu
├── wwwroot/                  # Web dosyaları
└── README.md                # Bu dosya
```

## 🛠️ Teknik Detaylar

### Windows Versiyonu
- **Framework**: .NET 8.0
- **UI**: Windows Forms
- **Dil**: C#
- **Animasyon**: Timer tabanlı 60 FPS
- **Grafik**: GDI+ ile çizim
- **Platform**: Windows only

### Web Versiyonu
- **Framework**: .NET 8.0 Blazor WebAssembly
- **UI**: HTML5 Canvas + Blazor
- **Dil**: C# + Razor
- **Animasyon**: JavaScript Timer
- **Grafik**: HTML5 Canvas
- **Platform**: Cross-platform (tarayıcı)

## 📸 Ekran Görüntüleri

Oyun 4 farklı tema ile çalışır:
- Gündüz teması: Mavi gökyüzü
- Günbatımı: Turuncu-kırmızı geçiş
- Gece: Koyu mavi gökyüzü + yıldızlar
- Şafak: Mor-pembe geçiş

## 🎨 Görsel Özellikler

- **Kuş**: Kırmızı renk, kanat çırpma animasyonu, detaylı göz ve gaga
- **Borular**: 3D metal görünüm, gölgeler, paslanma efektleri
- **Arka Plan**: Gradient renkler, tema değişimi
- **UI**: Gölgeli yazılar, temiz arayüz

## 📝 Lisans

Bu proje MIT lisansı altında lisanslanmıştır.

## 🤝 Katkıda Bulunma

1. Fork yapın
2. Feature branch oluşturun (`git checkout -b feature/AmazingFeature`)
3. Commit yapın (`git commit -m 'Add some AmazingFeature'`)
4. Push yapın (`git push origin feature/AmazingFeature`)
5. Pull Request oluşturun

## 📞 İletişim

Proje hakkında sorularınız için issue açabilirsiniz.
