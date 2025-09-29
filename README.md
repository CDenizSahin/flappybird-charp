# FlappyBird C# Oyunu

Bu proje, C# WinForms (.NET 8) kullanılarak geliştirilmiş modern bir FlappyBird oyunudur.

## 🎮 Oyun Özellikleri

- Kırmızı kuş (kanat çırpma animasyonu)
- 3D görünümlü borular (ışık/gölge, kapak, iç detay, pas efekti)
- Dinamik temalar: Gündüz → Günbatımı → Gece → Şafak (boru geçişlerinde değişir)
- Değişken zorluk: Rastgele yükseklikte borular ve boşluk
- Skor sistemi ve oyun bittiğinde yeniden başlatma

## 🎯 Kontroller

- Boşluk: Kuşu zıplat
- P: Oyunu durdur/devam ettir
- Oyun bittiğinde Boşluk: Yeniden başlat

## 🚀 Çalıştırma (Windows)

Gereksinimler:
- .NET 8.0 SDK
- Windows işletim sistemi

Visual Studio ile (Önerilen):
1. File → Open → Project/Solution
2. `FlappyBirdGame.csproj` dosyasını açın
3. F5 ile çalıştırın

Komut satırıyla:
```bash
# Projeyi klonlayın
git clone https://github.com/CDenizSahin/flappybird-charp.git
cd flappybird-charp

# Çalıştırın
dotnet run --project FlappyBirdGame.csproj
```

## 📁 Proje Yapısı
```
FlappyBirdGame/
├── FlappyBirdGame.csproj     # WinForms proje dosyası
├── Form1.cs                  # Oyun mantığı ve çizimler
├── Form1.Designer.cs         # WinForms designer
├── Program.cs                # Giriş noktası
└── README.md                 # Bu dosya
```

## 🛠️ Teknik Detaylar

- Framework: .NET 8.0 (net8.0-windows)
- UI: Windows Forms (WinForms)
- Dil: C#
- Animasyon: Timer tabanlı ~60 FPS
- Grafik: GDI+ ile çizim (DoubleBuffered)

## 📸 Temalar
- Gündüz: SkyBlue → LightCyan
- Günbatımı: OrangeRed → Gold
- Gece: MidnightBlue → DarkSlateBlue (yıldız efektleri)
- Şafak: MediumPurple → LightPink

## 📝 Lisans
MIT

## 🤝 Katkı
- Fork → Branch → Commit → PR akışını izleyin.
