namespace FlappyBirdGame;

using System.Drawing;
using System.Windows.Forms;

public partial class Form1 : Form
{
    private readonly Timer gameTimer;
    private readonly Random random;

    // Bird physics
    private float birdX;
    private float birdY;
    private float birdVelocityY;
    private const float Gravity = 0.5f;
    private const float FlapImpulse = -8.5f;
    private const int BirdSize = 32;
    
    // Bird animation
    private int wingFrame;
    private const int WingAnimationFrames = 8;

    // Pipes
    private readonly List<Rectangle> pipes;
    private int framesSinceLastPipe;
    private const int PipeSpawnFrames = 90; // ~1.5s at 60fps
    private const int PipeWidth = 70;
    private const int PipeGapMin = 130;
    private const int PipeGapMax = 180;
    private const int PipeSpeed = 3;

    // Score and theme
    private int score;
    private int themeIndex; // cycles backgrounds as player passes pipes

    // Game state
    private bool isRunning;
    private bool isGameOver;

    public Form1()
    {
        InitializeComponent();

        DoubleBuffered = true;
        KeyPreview = true;
        random = new Random();
        pipes = new List<Rectangle>();

        gameTimer = new Timer
        {
            Interval = 16 // ~60 FPS
        };
        gameTimer.Tick += OnGameTick;

        ResetGame();

        KeyDown += OnKeyDown;
        ResizeRedraw = true;
    }

    private void ResetGame()
    {
        birdX = 120f;
        birdY = ClientSize.Height > 0 ? ClientSize.Height / 2f : 240f;
        birdVelocityY = 0f;
        wingFrame = 0;

        pipes.Clear();
        framesSinceLastPipe = PipeSpawnFrames; // so we spawn quickly

        score = 0;
        themeIndex = 0;
        isGameOver = false;
        isRunning = true;
        gameTimer.Start();
        Invalidate();
    }

    private void OnKeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode == Keys.Space)
        {
            if (isGameOver)
            {
                ResetGame();
                return;
            }

            if (!isRunning)
            {
                isRunning = true;
                gameTimer.Start();
            }

            // Flap
            birdVelocityY = FlapImpulse;
        }
        else if (e.KeyCode == Keys.P)
        {
            // Pause/Resume
            isRunning = !isRunning;
            if (isRunning) gameTimer.Start(); else gameTimer.Stop();
        }
    }

    private void OnGameTick(object? sender, EventArgs e)
    {
        if (!isRunning || isGameOver)
            return;

        // Physics
        birdVelocityY += Gravity;
        birdY += birdVelocityY;
        
        // Wing animation
        wingFrame = (wingFrame + 1) % WingAnimationFrames;

        // Pipe spawning
        framesSinceLastPipe++;
        if (framesSinceLastPipe >= PipeSpawnFrames)
        {
            framesSinceLastPipe = 0;
            SpawnPipePair();
        }

        // Move pipes left and remove off-screen
        for (int i = 0; i < pipes.Count; i++)
        {
            Rectangle p = pipes[i];
            p.X -= PipeSpeed;
            pipes[i] = p;
        }
        pipes.RemoveAll(p => p.Right < 0);

        // Scoring and collision
        Rectangle birdRect = new Rectangle((int)birdX, (int)birdY, BirdSize, BirdSize);

        // Score when bird passes middle of a pipe pair (we look at bottom pipes only)
        for (int i = 0; i < pipes.Count; i += 2)
        {
            Rectangle top = pipes[i];
            if (i + 1 >= pipes.Count) break;
            Rectangle bottom = pipes[i + 1];

            // If bird just passed the right edge of bottom pipe
            if (bottom.Right < birdRect.Left && bottom.Right + PipeSpeed >= birdRect.Left)
            {
                score++;
                themeIndex = (themeIndex + 1) % ThemeCount;
            }
        }

        // Collision with ground/ceiling
        if (birdY < 0 || birdY + BirdSize > ClientSize.Height)
        {
            GameOver();
        }

        // Collision with pipes
        foreach (Rectangle p in pipes)
        {
            if (p.IntersectsWith(birdRect))
            {
                GameOver();
                break;
            }
        }

        Invalidate();
    }

    private void GameOver()
    {
        isGameOver = true;
        isRunning = false;
        gameTimer.Stop();
    }

    private void SpawnPipePair()
    {
        int height = ClientSize.Height;
        if (height <= 0)
            height = 480;

        int gap = random.Next(PipeGapMin, PipeGapMax + 1);
        int minTopMargin = 30;
        int maxTop = height - gap - 30; // leave bottom margin
        if (maxTop < minTopMargin) maxTop = minTopMargin;
        int topHeight = random.Next(minTopMargin, maxTop + 1);

        int x = ClientSize.Width + 10;

        Rectangle topPipe = new Rectangle(x, 0, PipeWidth, topHeight);
        Rectangle bottomPipe = new Rectangle(x, topHeight + gap, PipeWidth, height - (topHeight + gap));

        pipes.Add(topPipe);
        pipes.Add(bottomPipe);
    }

    protected override void OnPaint(PaintEventArgs e)
    {
        base.OnPaint(e);

        Graphics g = e.Graphics;
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

        DrawBackground(g);
        DrawPipes(g);
        DrawBird(g);
        DrawHud(g);

        if (!isRunning && !isGameOver)
        {
            DrawCenteredText(g, "Hazır? Boşluk: zıpla, P: durdur/devam", 18, Brushes.White, Brushes.Black);
        }
        if (isGameOver)
        {
            DrawCenteredText(g, $"Oyun Bitti! Skor: {score}  |  Boşluk ile yeniden başlat", 20, Brushes.Yellow, Brushes.Black);
        }
    }

    private void DrawBackground(Graphics g)
    {
        // Cycle themes: 0=Gündüz, 1=Günbatımı, 2=Gece, 3=Şafak
        switch (themeIndex)
        {
            case 0: // Day
                using (var lg = new System.Drawing.Drawing2D.LinearGradientBrush(ClientRectangle, Color.SkyBlue, Color.LightCyan, 90f))
                {
                    g.FillRectangle(lg, ClientRectangle);
                }
                break;
            case 1: // Sunset
                using (var lg = new System.Drawing.Drawing2D.LinearGradientBrush(ClientRectangle, Color.OrangeRed, Color.Gold, 90f))
                {
                    g.FillRectangle(lg, ClientRectangle);
                }
                break;
            case 2: // Night
                using (var lg = new System.Drawing.Drawing2D.LinearGradientBrush(ClientRectangle, Color.MidnightBlue, Color.DarkSlateBlue, 90f))
                {
                    g.FillRectangle(lg, ClientRectangle);
                }
                // Stars
                for (int i = 0; i < 30; i++)
                {
                    int sx = (i * 53) % (ClientSize.Width + 1);
                    int sy = ((i * 97) % (ClientSize.Height + 1));
                    g.FillEllipse(Brushes.White, sx, sy, 2, 2);
                }
                break;
            case 3: // Dawn
            default:
                using (var lg = new System.Drawing.Drawing2D.LinearGradientBrush(ClientRectangle, Color.MediumPurple, Color.LightPink, 90f))
                {
                    g.FillRectangle(lg, ClientRectangle);
                }
                break;
        }
        // Ground line
        using var groundPen = new Pen(Color.FromArgb(80, Color.Black), 2f);
        g.DrawLine(groundPen, 0, ClientSize.Height - 1, ClientSize.Width, ClientSize.Height - 1);
    }

    private void DrawBird(Graphics g)
    {
        Rectangle bird = new Rectangle((int)birdX, (int)birdY, BirdSize, BirdSize);
        
        // Main body - red color
        using var bodyBrush = new SolidBrush(Color.Red);
        using var bodyOutlineBrush = new SolidBrush(Color.DarkRed);
        using var eyeBrush = new SolidBrush(Color.White);
        using var pupilBrush = new SolidBrush(Color.Black);
        using var beakBrush = new SolidBrush(Color.Orange);
        using var wingBrush = new SolidBrush(Color.DarkRed);

        // Body with outline
        g.FillEllipse(bodyBrush, bird);
        using var outlinePen = new Pen(Color.DarkRed, 2f);
        g.DrawEllipse(outlinePen, bird);

        // Wing animation - different positions based on frame
        float wingOffset = (float)Math.Sin(wingFrame * Math.PI / 4) * 3; // Wing flapping motion
        Rectangle wing = new Rectangle(bird.X + BirdSize / 4, bird.Y + BirdSize / 3 + (int)wingOffset, BirdSize / 2, BirdSize / 3);
        g.FillEllipse(wingBrush, wing);

        // Eye with better detail
        var eye = new Rectangle(bird.X + BirdSize / 2, bird.Y + BirdSize / 4, BirdSize / 4, BirdSize / 4);
        g.FillEllipse(eyeBrush, eye);
        using var eyeOutlinePen = new Pen(Color.Black, 1f);
        g.DrawEllipse(eyeOutlinePen, eye);
        
        var pupil = new Rectangle(eye.X + eye.Width / 3, eye.Y + eye.Height / 3, eye.Width / 3, eye.Height / 3);
        g.FillEllipse(pupilBrush, pupil);

        // Beak with better shape
        Point p1 = new Point(bird.Right, bird.Y + BirdSize / 2);
        Point p2 = new Point(bird.Right + BirdSize / 3, bird.Y + BirdSize / 2 - BirdSize / 6);
        Point p3 = new Point(bird.Right + BirdSize / 3, bird.Y + BirdSize / 2 + BirdSize / 6);
        g.FillPolygon(beakBrush, new[] { p1, p2, p3 });
        using var beakPen = new Pen(Color.DarkOrange, 1f);
        g.DrawPolygon(beakPen, new[] { p1, p2, p3 });
    }

    private void DrawPipes(Graphics g)
    {
        for (int i = 0; i < pipes.Count; i++)
        {
            Rectangle p = pipes[i];
            DrawRealisticPipe(g, p);
        }
    }

    private void DrawRealisticPipe(Graphics g, Rectangle pipe)
    {
        // Main pipe body with gradient
        using var pipeGradient = new System.Drawing.Drawing2D.LinearGradientBrush(
            pipe, Color.DarkGreen, Color.ForestGreen, 0f);
        g.FillRectangle(pipeGradient, pipe);

        // 3D effect - highlight on top and left edges
        using var highlightPen = new Pen(Color.LightGreen, 2f);
        g.DrawLine(highlightPen, pipe.Left, pipe.Top, pipe.Right, pipe.Top);
        g.DrawLine(highlightPen, pipe.Left, pipe.Top, pipe.Left, pipe.Bottom);

        // 3D effect - shadow on bottom and right edges
        using var shadowPen = new Pen(Color.DarkGreen, 2f);
        g.DrawLine(shadowPen, pipe.Right, pipe.Top, pipe.Right, pipe.Bottom);
        g.DrawLine(shadowPen, pipe.Left, pipe.Bottom, pipe.Right, pipe.Bottom);

        // Inner pipe detail
        Rectangle innerPipe = new Rectangle(pipe.X + 8, pipe.Y + 8, pipe.Width - 16, pipe.Height - 16);
        using var innerBrush = new SolidBrush(Color.FromArgb(120, Color.Black));
        g.FillRectangle(innerBrush, innerPipe);

        // Pipe cap (top/bottom edge)
        Rectangle cap = new Rectangle(pipe.X - 5, pipe.Y - 8, pipe.Width + 10, 16);
        if (pipe.Y == 0) // Top pipe
        {
            cap = new Rectangle(pipe.X - 5, pipe.Bottom - 8, pipe.Width + 10, 16);
        }
        
        using var capGradient = new System.Drawing.Drawing2D.LinearGradientBrush(
            cap, Color.DarkGreen, Color.ForestGreen, 0f);
        g.FillRectangle(capGradient, cap);
        
        // Cap highlight
        using var capHighlightPen = new Pen(Color.LightGreen, 1f);
        g.DrawLine(capHighlightPen, cap.Left, cap.Top, cap.Right, cap.Top);
        g.DrawLine(capHighlightPen, cap.Left, cap.Top, cap.Left, cap.Bottom);
        
        // Cap shadow
        using var capShadowPen = new Pen(Color.DarkGreen, 1f);
        g.DrawLine(capShadowPen, cap.Right, cap.Top, cap.Right, cap.Bottom);
        g.DrawLine(capShadowPen, cap.Left, cap.Bottom, cap.Right, cap.Bottom);

        // Rust/weathering effect
        using var rustBrush = new SolidBrush(Color.FromArgb(30, Color.Brown));
        for (int j = 0; j < 3; j++)
        {
            int rustX = pipe.X + (j * 20) % (pipe.Width - 10);
            int rustY = pipe.Y + (j * 15) % (pipe.Height - 10);
            g.FillEllipse(rustBrush, rustX, rustY, 3, 3);
        }
    }

    private void DrawHud(Graphics g)
    {
        using var textBrush = new SolidBrush(Color.White);
        using var shadowBrush = new SolidBrush(Color.FromArgb(120, Color.Black));
        string scoreText = $"Skor: {score}";
        using var f = new Font(Font.FontFamily, 14f, FontStyle.Bold);
        var size = g.MeasureString(scoreText, f);
        PointF pos = new PointF(10, 10);
        g.DrawString(scoreText, f, shadowBrush, pos.X + 2, pos.Y + 2);
        g.DrawString(scoreText, f, textBrush, pos);
    }

    private void DrawCenteredText(Graphics g, string text, float fontSize, Brush fill, Brush shadow)
    {
        using var f = new Font(Font.FontFamily, fontSize, FontStyle.Bold);
        var size = g.MeasureString(text, f);
        PointF pos = new PointF((ClientSize.Width - size.Width) / 2f, (ClientSize.Height - size.Height) / 2f);
        g.DrawString(text, f, shadow, pos.X + 2, pos.Y + 2);
        g.DrawString(text, f, fill, pos);
    }

    private int ThemeCount => 4;
}
