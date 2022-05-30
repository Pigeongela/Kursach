using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using ZedGraph;

namespace Kursach
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            Form3 f3= new Form3();
            f3.Owner = this.Owner as Form3;
        }
        Random rnd = new Random();

        double xmin, xmax; //интервал, на котором строится график выбранной функции
        string[] f_name = {"cos(x)", "x^n", "sin(x)", "e^x", "a^x"};//список функций
        LineItem myCurve;

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Form2 f2 = new Form2();
            f2.ShowDialog();
        }

        //Значения x,i,n,k Для функций передаюься из Form3
        private double f(double x, int i, int n) //строим график выбранной функции
        {
            if (i == 1)
            {
                return Math.Pow(x, n); //x^n
            }
            if (i == 3)
            {
                return Math.Pow(Math.E,x); //e^x
            }
            if (i == 4)
            {
                return Math.Pow(n,x); //a^x
            }
            if (i == 2)
            {
                return Math.Sin(x); //sin x
            }
            if (i == 0)
            {
                return Math.Cos(x); //cos x
            }
            return 0;
        }

        public void Func(double a, double b, int i, int n, double k)
        {
            //Получим панель для рисования
            GraphPane pane = zedGraphControl1.GraphPane;
            //Очистим список кривых на тот случай, если до этого сигналы уже были нарисованы
            pane.CurveList.Clear();
            //Создадим список точек
            PointPairList f_list = new PointPairList();//для графика функции

            double x;

            xmin = a - 1;
            xmax = b + 1;

            //Заполняем список точек
            for (x = xmin; x <= xmax; x+=0.5)
            {
                // добавим в список точку
                f_list.Add(x, k*f(x,i,n));
            }
            
            //Создадим кривую с названием выбранного графика, которая будет рисоваться голубым цветом, выделим опорные точки окружностями
            myCurve = pane.AddCurve(f_name[i], f_list, Color.Blue, SymbolType.Circle);

            //Вызываем метод AxisChange (), чтобы обновить данные об осях.
            zedGraphControl1.AxisChange();

            // Обновляем график
            zedGraphControl1.Invalidate();
        }

        public void DrawRect(double a, double b, int i, int n, double k) //метод серединных прямоугольников
        {
            // Получим панель для рисования
            GraphPane pane = zedGraphControl1.GraphPane;
            // Создадим список точек
            PointPairList f2_list = new PointPairList();//для выбранного метода

            double l;
            l = (b - a) / 2; //середина интервала

            //Строим прямоугольник
                f2_list.Add(a, 0);
                f2_list.Add(a, k*f(a + l,i,n));
                f2_list.Add(b, k*f(a+l,i,n));
                f2_list.Add(b, 0);
                f2_list.Add(a, 0);

            LineItem myCurve2 = pane.AddCurve("Метод прямоугольников", f2_list, Color.Red, SymbolType.None);
            //Установим заливку для кривой
            myCurve2.Line.Fill = new ZedGraph.Fill(Color.Red);

            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
        }

        public void RectangleMethod(double a,double b,int i,int n, double k) //рассчет значения по методу серединных прямоугольников
        {
            //вычисляем шаг
            double l = (b - a) / 20000;
            //инициализируем переменную, в которую будет запоминаться значение интеграла
            double Integral = 0;
            //производим пошаговое приращение переменной на слагаемое, определенное методом прямоугольников
            for (double x = a + l; x <= b; x += l)
            {
                //у нечётных функций на интервале [-a;a] интеграл = 0
                if (b == -a)
                {
                    if (n % 2 != 0 || i == 3 || i == 1)
                    {
                        Integral = 0;
                        break;
                    }
                }
                Integral += l * k * f(x, i, n);
            }
            //вывод результата
            textBox1.Text += '\n' + Convert.ToString(Integral);
        }

        public void DrawTrap(double a, double b, int i, int n, double k) //метод трапеций
        {
            GraphPane pane = zedGraphControl1.GraphPane;
            PointPairList f2_list = new PointPairList();

            //Строим трапецию
            f2_list.Add(a, 0);
            f2_list.Add(a, k*f(a,i,n));
            f2_list.Add(b, k*f(b,i,n));
            f2_list.Add(b, 0);
            f2_list.Add(a, 0);

            LineItem myCurve2 = pane.AddCurve("Метод трапеций", f2_list, Color.Green, SymbolType.None);
            myCurve2.Line.Fill = new ZedGraph.Fill(Color.Green);
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
        }

        public void TrapMethod(double a, double b, int i, int n, double k)//рассчет значения по методу трапеций
        {
            //вычисляем шаг
            double l = (b - a) / 20000;
            //инициализируем переменную, в которую будет запоминаться значение интеграла
            double Integral = 0;
            //производим пошаговое приращение переменной на слагаемое, определенное методом трапеций
            for (double x = a + l; x <= b; x += l)
            {
                //у нечётных функций на интервале [-a;a] интеграл = 0
                if (b == -a)
                {
                    if (n % 2 != 0 || i == 3 || i == 1)
                    {
                        Integral = 0;
                        break;
                    }
                }
                Integral += l * k * (f(x, i, n) + (f(x, i, n) + l)) / 2;
            }
            //вывод результата
            textBox1.Text += '\n' + Convert.ToString(Integral);
        }

        public void DrawSimp(double a, double b, int i, int n, double k) //метод Симпсона (парабол)
        {
            GraphPane pane = zedGraphControl1.GraphPane;
            PointPairList f2_list = new PointPairList();//для выбранного метода

            for (a = a; a <= b; a++)
            {
                f2_list.Add(a, k * f(a, i, n));
            }

            LineItem myCurve2 = pane.AddCurve("Метод Симпсона", f2_list, Color.Red, SymbolType.None);
            myCurve2.Line.Fill = new ZedGraph.Fill(Color.Yellow);
            zedGraphControl1.AxisChange();
            zedGraphControl1.Invalidate();
        }

        public void SimpMethod(double a, double b, int i, int n, double k)
        {
            //вычисляемшаг
            double l = (b - a) / (2 * 20000);
            /*инициализируем переменную, в которую будет запоминаться значение интеграла, 
                        заранее учитывая известные слагаемые*/
            double Integral = (a * k * f(a, i, n)) + (b * k * f(b, i, n));
            //задаём счётчик слагаемых, так как коэффиценты чётных и нечётных слагаемых различны
            int j = 1;
            //производим пошаговое приращение переменной на слагаемое, определенное методом Симсона
            for (double x = a + l; x < b; x += l)
            {
                if (j % 2 == 1)
                {
                    Integral += 4 * k * f(x, i, n);
                    j++;
                }
                else
                {
                    Integral += 2 * k * f(x, i, n);
                    j++;
                }
            }
            Integral *= l / 3;
            //выводрезультата
            textBox1.Text += '\n' + Convert.ToString(Integral);
        }


        private void Form1_Load(object sender, EventArgs e)
        {
            XY(); 
        }

        //рисуем разметку
        private void XY()
        {
            GraphPane pane = zedGraphControl1.GraphPane;

            // Устанавливаем интересующий нас интервал по оси Y
            pane.YAxis.Scale.Min = -8;
            //pane.YAxis.Scale.Max = ymax_limit;

            // !!!
            // Включаем отображение сетки напротив крупных рисок по оси X
            pane.XAxis.MajorGrid.IsVisible = true;

            // Задаем вид пунктирной линии для крупных рисок по оси X:
            // Длина штрихов равна 10 пикселям, ...
            pane.XAxis.MajorGrid.DashOn = 10;

            // затем 5 пикселей - пропуск
            pane.XAxis.MajorGrid.DashOff = 5;


            // Включаем отображение сетки напротив крупных рисок по оси Y
            pane.YAxis.MajorGrid.IsVisible = true;

            // Аналогично задаем вид пунктирной линии для крупных рисок по оси Y
            pane.YAxis.MajorGrid.DashOn = 10;
            pane.YAxis.MajorGrid.DashOff = 5;

            // Включаем отображение сетки напротив мелких рисок по оси X
            pane.YAxis.MinorGrid.IsVisible = true;

            // Задаем вид пунктирной линии для крупных рисок по оси Y:
            // Длина штрихов равна одному пикселю, ...
            pane.YAxis.MinorGrid.DashOn = 1;

            // затем 2 пикселя - пропуск
            pane.YAxis.MinorGrid.DashOff = 2;

            // Включаем отображение сетки напротив мелких рисок по оси Y
            pane.XAxis.MinorGrid.IsVisible = true;
            // Аналогично задаем вид пунктирной линии для крупных рисок по оси Y
            pane.XAxis.MinorGrid.DashOn = 1;
            pane.XAxis.MinorGrid.DashOff = 2;
            // Вызываем метод AxisChange (), чтобы обновить данные об осях.
            // В противном случае на рисунке будет показана только часть графика,
            // которая умещается в интервалы по осям, установленные по умолчанию
            zedGraphControl1.AxisChange();

            // Обновляем график
            zedGraphControl1.Invalidate();
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {            
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Text files(*.text)|*.txt|All files(*.*)|*.*";
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            string filename = saveFileDialog1.FileName;
            System.IO.File.WriteAllText(filename, textBox1.Text);
            MessageBox.Show("Файл сохранен");
            zedGraphControl1.SaveAsBitmap();
        }

        private void очиститьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Получим панель для рисования
            GraphPane pane = zedGraphControl1.GraphPane;

            // Очистим список кривых на тот случай, если до этого сигналы уже были нарисованы
            pane.CurveList.Clear();
            zedGraphControl1.Invalidate();
        }

        private void помощьToolStripMenuItem_Click(object sender, EventArgs e)
        { 
            MessageBox.Show("1) Нажмите кнопку 'Выбрать интеграл'\n\n2) В появившемся окне выберите нужный интеграл\n\n3) Выберите и введите нужные значения:\n k - константа\n n - степень (для степенных функций)\n а - основание (для показательных функций)\n\nТакже введите промежуток интегрирования и выберите метод\n");
        }

    }
}
