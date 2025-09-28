# FlappyBird C# Oyunu

Bu proje, C# WinForms kullanılarak geliştirilmiş modern bir FlappyBird oyunudur.

## 🎮 Oyun Özellikleri

- **Kırmızı Kuş**: Gerçekçi kanat çırpma animasyonu ile
- **3D Borular**: Gerçekçi metal boru görünümü, gölgeler ve detaylar
- **Dinamik Temalar**: 4 farklı arka plan teması
  - 🌅 Gündüz
  - 🌇 Günbatımı  
  - 🌙 Gece (yıldızlı)
  - 🌄 Şafak
- **Değişken Zorluk**: Rastgele yükseklikte borular
- **Skor Sistemi**: Her boru geçişinde skor artışı

## 🎯 Kontroller

- **Boşluk Tuşu**: Kuşu zıplat
- **P Tuşu**: Oyunu durdur/devam ettir
- **Oyun Bittiğinde Boşluk**: Yeniden başlat

## 🚀 Çalıştırma

### Gereksinimler
- .NET 8.0 SDK
- Windows işletim sistemi

### Yöntem 1: GitHub Codespaces (Önerilen)
1. Bu repository'yi fork edin
2. **Code** → **Codespaces** → **Create codespace**
3. Terminal'de: `dotnet run`

### Yöntem 2: Yerel Kurulum
```bash
# Projeyi klonlayın
git clone https://github.com/CDenizSahin/flappybird-charp.git
cd flappybird-charp

# Projeyi derleyin
dotnet build

# Oyunu çalıştırın
dotnet run
```

### Yöntem 3: Visual Studio
1. **File** → **Open** → **Project/Solution**
2. `FlappyBirdGame.csproj` dosyasını açın
3. **F5** tuşuna basın

## 🛠️ Teknik Detaylar

- **Framework**: .NET 8.0
- **UI**: Windows Forms
- **Dil**: C#
- **Animasyon**: Timer tabanlı 60 FPS
- **Grafik**: GDI+ ile çizim

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
