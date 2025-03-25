using System;
using System.Collections.Generic;
using System.Linq;
using System.Drawing;
using System.Threading.Tasks;
using System.Windows.Forms;
public class KochovaVlocka : Form
{
        // Nastavení výchozí hodnoty rekurze na 1
        private int Hloubka = 0;
        // Numerické pole pro výběr úrovně rekurze
        private NumericUpDown vyberHloubky;
        // Rozbalovací nabídka pro výběr tvaru
        private ComboBox vyberTvaru;
        // Pole barev pro vykreslování
        private Color[] barvy = { Color.Blue, Color.Red, Color.Green, Color.Purple, Color.Orange, Color.Brown, Color.Cyan, Color.Magenta, Color.Black };
        // Výchozí vybraný tvar;
        private string vybranyTvar = "Kochova vločka";
        // Uložení bodů pro iterace kapradiny
        private Dictionary<int, List<PointF>> iteraceKapradiny = new Dictionary<int, List<PointF>>();
        // Náhodný generátor pro kapradinu
        private Random random = new Random();


    public KochovaVlocka()
    {
        // Nastavení titulku okna na "Kochova vločka"
        this.Text = "Kochova vločka";

        // Nastavení velikosti okna na 800x800 pixelů
        this.Size = new Size(1600, 1600);

        // Připojení metody NakresliTvar k události Paint, aby se tvar vykreslil při překreslení okna
        this.Paint += new PaintEventHandler(NakresliTvar);

        // Vytvoření prvku pro výběr hloubky rekurze
        vyberHloubky = new NumericUpDown()
        {
            // Nastavení minimální hodnoty na 0
            Minimum = 0,

            // Nastavení maximální hodnoty na 10000 (bude upraveno podle vybraného tvaru)
            Maximum = 10000,

            // Inicializace hodnoty na aktuální hloubku
            Value = Hloubka,

            // Umístění NumericUpDown v okně na souřadnice (10, 10)
            Location = new Point(10, 10)
        };

        // Přidání události, která reaguje na změnu hodnoty
        vyberHloubky.ValueChanged += (sender, e) => {
            // Aktualizace proměnné Hloubka na novou hodnotu
            Hloubka = (int)vyberHloubky.Value;

            // Pokud je vybrán "Náhodné generování" a daná hloubka ještě není vygenerovaná
            if (vybranyTvar == "Náhodné generování" && !iteraceKapradiny.ContainsKey(Hloubka))
            {
                // Vygeneruj kapradinu pro tuto hloubku
                iteraceKapradiny[Hloubka] = GenerujKapradinu(Hloubka);
            }

            // Vyzvání okna k překreslení, aby se aktualizoval vykreslený tvar
            this.Invalidate();
        };

        // Přidání prvku NumericUpDown do formuláře
        this.Controls.Add(vyberHloubky);

        // Vytvoření ComboBoxu pro výběr tvaru
        vyberTvaru = new ComboBox()
        {
            // Umístění ComboBoxu v okně na souřadnice (10, 40)
            Location = new Point(10, 40),

            // Nastavení šířky ComboBoxu na 150 pixelů
            Width = 150,

            // Nastavení stylu ComboBoxu, aby uživatel mohl vybírat pouze z dostupných možností
            DropDownStyle = ComboBoxStyle.DropDownList
        };

        // Přidání možností do seznamu
        vyberTvaru.Items.AddRange(new string[] {"Kochova vločka","Fibonacciho spirála","Fraktální strom","Náhodné generování","IFS fraktál","Draci spirala"});

        // Nastavení výchozí vybrané možnosti na první položku (Kochova vločka)
        vyberTvaru.SelectedIndex = 0;

        // Přidání události pro změnu vybrané položky v ComboBoxu
        vyberTvaru.SelectedIndexChanged += (sender, e) => {
            // Uložení vybraného tvaru do proměnné vybranyTvar
            vybranyTvar = vyberTvaru.SelectedItem.ToString();

            // Pokud je vybrána "Kochova vločka"
            if (vybranyTvar == "Kochova vločka")
            {
                // Omezíme maximální hloubku na 8 (protože vyšší hodnoty mohou být výpočetně náročné)
                vyberHloubky.Maximum = 8;
            }
            // Pokud je vybrána "Fibonacciho spirála"
            else if (vybranyTvar == "Fibonacciho spirála")
            {
                // Omezíme maximální hloubku na 15 (protože Fibonacciho spirála snese hlubší rekurzi)
                vyberHloubky.Maximum = 15;
            }
            // Pokud je vybrán "Fraktální strom"
            else if (vybranyTvar == "Fraktální strom")
            {
                // Maximální hloubka pro fraktální strom nastavena na 13
                vyberHloubky.Maximum = 13;
            }
            // Pokud je vybrán "Náhodné generování"
            else if (vybranyTvar == "Náhodné generování")
            {
                // Maximální hloubka pro testovací fraktál nastavena na 10
                vyberHloubky.Maximum = 10;
            }
            else if (vybranyTvar == "IFS fraktál")
            {
                    vyberHloubky.Maximum = 10;
            }
            else if (vybranyTvar == "Draci spirala")
            {
                    vyberHloubky.Maximum = 20;
            }
            // Resetujeme hloubku na 0 při změně tvaru
            vyberHloubky.Value = 0;
            // Překreslíme okno, aby se zobrazil nový tvar
            this.Invalidate(); 
            };
            // Přidání ComboBoxu do formuláře
            this.Controls.Add(vyberTvaru);
        }
    
        private void NakresliTvar(object sender, PaintEventArgs e)
        {
            // Zkontroluje, jaký tvar je vybrán
            if (vybranyTvar == "Kochova vločka")
            {
                // Pokud je vybrána Kochova vločka, zavolá metodu NakresliVlocku
                NakresliVlocku(e.Graphics);
            }
            else if (vybranyTvar == "Fibonacciho spirála")
            {
                // Pokud je vybrána Fibonacciho spirála, zavolá metodu NakresliFibonacci
                NakresliFibonacci(e.Graphics);
            }
            else if (vybranyTvar == "Fraktální strom")
            {
                NakresliStrom(e.Graphics);
            }
            else if (vybranyTvar == "Náhodné generování")
            {
                NakresliKapradinu(e.Graphics);
            }
            else if (vybranyTvar == "IFS fraktál")
            {
                NakresliFraktal(e.Graphics);
            }
            else if (vybranyTvar == "Draci spirala")
            {
                NakresliDraciSpiralu(e.Graphics);
            }
        }

        private void KresliUhel(Graphics g, int hloubka2, PointF p1, PointF p2, int colorIndex)
        {
            // Pokud je dosažena základní úroveň rekurze (hloubka2 == 0), vykreslí přímku mezi body p1 a p2
            if (hloubka2 == 0)
            {
            // Vytvoření pera s barvou z pole barvy podle indexu colorIndex
            using (Pen pen = new Pen(barvy[colorIndex % barvy.Length], 1))
            {
                // Vykreslení úsečky mezi body p1 a p2
                g.DrawLine(pen, p1, p2);
            }
            }
            else
        {
            // Vypočítání třetinových bodů mezi p1 a p2
            // Rozdíl X souřadnic dělený třemi (pro třetiny úsečky)
            float dx = (p2.X - p1.X) / 3;
            // Rozdíl Y souřadnic dělený třemi
            float dy = (p2.Y - p1.Y) / 3;
            // První třetinový bod
            PointF a = new PointF(p1.X + dx, p1.Y + dy);
            // Druhý třetinový bod
            PointF b = new PointF(p1.X + 2 * dx, p1.Y + 2 * dy);
            // Vypočítání bodu C, který vytváří špičku trojúhelníku v Kochově křivce
            // Úhel 60 stupňů (PI / 3 radianů)
            float angle = (float)(Math.PI / 3);
            // Výpočet nové X souřadnice bodu C
            float x = (float)(a.X + (b.X - a.X) * Math.Cos(angle) - (b.Y - a.Y) * Math.Sin(angle));
            // Výpočet nové Y souřadnice bodu C
            float y = (float)(a.Y + (b.X - a.X) * Math.Sin(angle) + (b.Y - a.Y) * Math.Cos(angle));
            // Vytvoření bodu C
            PointF c = new PointF(x, y);
            // Rekurzivní volání pro čtyři části rozdělené úsečky
            // Levá část
            KresliUhel(g, hloubka2 - 1, p1, a, colorIndex + 1);
            // První úsek trojúhelníku
            KresliUhel(g, hloubka2 - 1, a, c, colorIndex + 1);
            // Druhý úsek trojúhelníku
            KresliUhel(g, hloubka2 - 1, c, b, colorIndex + 1);
            // Pravá část
            KresliUhel(g, hloubka2 - 1, b, p2, colorIndex + 1);
        }
    }


    private void NakresliVlocku(Graphics g)
    {
        // Nastavení vykreslování s vyhlazováním hran (AntiAlias) pro lepší kvalitu kreslení
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        // Definování třech hlavních bodů rovnostranného trojúhelníku
        // Levý dolní roh
        PointF p1 = new PointF(200, 600);
        // Pravý dolní roh
        PointF p2 = new PointF(600, 600);
        // Horní vrchol
        PointF p3 = new PointF(400, 200);
        // Rekurzivně vykreslí každou stranu Kochovy vločky
        // Spodní strana trojúhelníku
        KresliUhel(g, Hloubka, p1, p2, 0);
        // Pravá strana trojúhelníku
        KresliUhel(g, Hloubka, p2, p3, 0);
        // Levá strana trojúhelníku
        KresliUhel(g, Hloubka, p3, p1, 0);
    }


    private void NakresliFibonacci(Graphics g)
        {
            // Nastavení vyhlazování hran pro lepší kvalitu kreslení
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            // Středová pozice spirály
            PointF prostredek = new PointF(400, 400);
            // Počáteční úhel (začíná na 0 radianech)
            float uhel2 = 0;
            // Měřítko zvětšení (násobitel pro Fibonacciho čísla)
            float meritko = 2.5f;
            List<PointF> body = new List<PointF>();
            // První bod je střed spirály
            PointF predchoziPoint = prostredek;
            // Procházení Fibonacciho čísel až do požadované hloubky
            for (int i = 1; i < Hloubka + 1; i++)
            {
                // Vypočítá délku dalšího úseku podle Fibonacciho čísla
                float polomer = meritko * Fibonacci(i);
                // Vypočítá X souřadnici nového bodu
                float x = prostredek.X + (float)(polomer * Math.Cos(uhel2));
                // Vypočítá Y souřadnici nového bodu
                float y = prostredek.Y + (float)(polomer * Math.Sin(uhel2));
                // Vytvoří nový bod na základě výpočtu           
                body.Add(new PointF(x, y));
                // Posune úhel o 90° (PI/2 radianů), aby se spirála tvořila ve čtvercovém vzoru
                uhel2 += (float)(Math.PI / 2);
            }
            // Vytvoření pera s černou barvou a tloušťkou 1 pixel
            if (body.Count > 1)
            {
                using (Pen pen = new Pen(Color.Black, 1))
                {
                    // Kreslení hladké křivky přes body v seznamu "body"
                    // - `body.ToArray()` převede seznam na pole bodů (Point[])
                    // - `0.5f` je napětí (tension), které určuje zakřivení křivky
                    g.DrawCurve(pen, body.ToArray(), 0.5f);
                }
            }
        }
        private void NakresliStrom(Graphics g)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            PointF start = new PointF(400, 700);
            NakresliVetve(g, Hloubka, start, -90, 120);
        }
        private void NakresliVetve(Graphics g, int hloubka, PointF start, double uhel, double delka)
        {
            // Pokud je hloubka 0, ukončíme rekurzi
            if (hloubka == 0) return;
            // Vypočítáme koncový bod větve na základě délky a úhlu
            PointF konec = new PointF( 
                // Posun X souřadnice
                (float)(start.X + delka * Math.Cos(uhel * Math.PI / 180)),
                // Posun Y souřadnice
                (float)(start.Y + delka * Math.Sin(uhel * Math.PI / 180))
            );
                // Vytvoříme pero s barvou hnědou a tloušťkou odpovídající hloubce
                using (Pen pen = new Pen(Color.Brown, hloubka))
                // Nakreslíme čáru od počátečního bodu ke koncovému
                g.DrawLine(pen, start, konec);

            // Rekurzivně voláme funkci pro dvě nové větve, které se odklánějí od hlavní větve
             // Levá větev
            NakresliVetve(g, hloubka - 1, konec, uhel - 30, delka * 0.7);
            // Pravá větev
            NakresliVetve(g, hloubka - 1, konec, uhel + 30, delka * 0.7);
        }
        private List<PointF> GenerujKapradinu(int iterace)
        {
            List<PointF> body = new List<PointF>();
            float x = 0, y = 0;
            for (int i = 0; i < iterace * 100; i++)
            {
                double r = random.NextDouble();
                float newX, newY;

                if (r < 0.01)
                {
                    newX = 0;
                    newY = 0.16f * y;
                }
                else if (r < 0.86)
                {
                    newX = 0.85f * x + 0.04f * y;
                    newY = -0.04f * x + 0.85f * y + 1.6f;
                }
                else if (r < 0.92)
                {
                    newX = 0.2f * x - 0.26f * y;
                    newY = 0.23f * x + 0.22f * y + 1.6f;
                }
                else
                {
                    newX = -0.15f * x + 0.28f * y;
                    newY = 0.26f * x + 0.24f * y + 0.44f;
                }

                x = newX;   
                y = newY;
                body.Add(new PointF(500 + x * 50, 900 - y * 50));
            }
            return body;
        }
        private void NakresliFraktal(Graphics g)
        {
            List<(float a, float b, float c, float d, float e, float f, float probability)> transformace = new List<(float, float, float, float, float, float, float)>
            {
                (0.0f, -0.5f, 0.5f, 0.0f, 0.5f, 0.0f, 1f / 3f),
                (0.0f, 0.5f, -0.5f, 0.0f, 0.0f, 0.5f, 1f / 3f),
                (0.5f, 0.0f, 0.0f, 0.5f, 0.25f, 0.5f, 1f / 3f)
            };

            List<PointF> points = new List<PointF>();
            float x_n = 0, y_n = 0;

            for (int i = 0; i < 1000; i++)
            {
                double r = random.NextDouble();
                float x_next = 0, y_next = 0;

                foreach (var (a, b, c, d, e, f, p) in transformace)
                {
                    if (r < p)
                    {
                        x_next = a * x_n + b * y_n + e;
                        y_next = c * x_n + d * y_n + f;
                        break;
                    }
                    r -= p;
                }
                x_n = x_next;
                y_n = y_next;
                points.Add(new PointF(350 + x_n * 500, 650 - y_n * 500));
            }

            using (Pen pen = new Pen(Color.Black, 1))
            {
                foreach (var point in points)
                    g.DrawRectangle(pen, point.X, point.Y, 1, 1);
            }
        }
    private void NakresliDraciSpiralu(Graphics g)
    {
        g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
        PointF start = new PointF(300, 400);
        PointF end = new PointF(500, 400);
        KresliDraciKrivku(g, Hloubka, start, end, true);
    }

    private void KresliDraciKrivku(Graphics g, int hloubka, PointF p1, PointF p2, bool left)
    {
        if (hloubka == 0)
        {
            using (Pen pen = new Pen(Color.Black, 1))
            {
                g.DrawLine(pen, p1, p2);
            }
        }
        else
        {
            float dx = (p2.X - p1.X) / 2;
            float dy = (p2.Y - p1.Y) / 2;
            PointF mid = new PointF(
                p1.X + dx - (left ? dy : -dy),
                p1.Y + dy + (left ? dx : -dx)
            );
            KresliDraciKrivku(g, hloubka - 1, p1, mid, true);
            KresliDraciKrivku(g, hloubka - 1, mid, p2, false);
        }
    }


    private void NakresliKapradinu(Graphics g)
        {
            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            if (!iteraceKapradiny.ContainsKey(Hloubka))
            {
                iteraceKapradiny[Hloubka] = GenerujKapradinu(Hloubka);
            }

            using (Pen pen = new Pen(Color.Green, 1))
            {
                List<PointF> body = iteraceKapradiny[Hloubka];
                for (int i = 1; i < body.Count - 1; i++)
                {
                    // Změněno z bodů na spojité čáry
                    g.DrawLine(pen, body[i], body[i + 1]);
                }
            }
        }
        private int Fibonacci(int n)
        {
            // Pokud je n 0 nebo 1, vrací n (základní případ Fibonacciho posloupnosti)
            if (n <= 1) return n;
            // Výpočet Fibonacciho čísla rekurzivně
            return Fibonacci(n - 1) + Fibonacci(n - 2); 
        }
        // Hlavní metoda aplikace
        public static void Main() 
        {
            // Spuštění aplikace s formulářem KochovaVlocka
            Application.Run(new KochovaVlocka());   
        }
    }
