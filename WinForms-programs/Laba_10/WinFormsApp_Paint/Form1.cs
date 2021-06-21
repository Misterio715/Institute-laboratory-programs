using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing.Drawing2D;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WinFormsApp_Paint
{
    public partial class Form1 : Form
    {
        
        //Параметры рисования
        int RX,  //Размер по горизонтали
            RY;  //Размер по вертикали
        Graphics контекст;  //Параметры изображения по умолчанию
        Pen перо;
        SolidBrush кисть;
        Color Фон, Полоса;
        bool Flag = false;
        Pen blackPen = new Pen(Color.Black, 10);


        Color color = Color.Black; //Создаем переменную типа Color присваиваем ей черный цвет.
        bool isPressed = false; //логическая переменная понадобиться для опеределения когда можно рисовать на panel
        Point CurrentPoint; //Текущая точка ресунка.
        Point PrevPoint; //Это начальная точка рисунка.
        Graphics g; //Создаем графический элемент.
        ColorDialog colorDialog = new ColorDialog(); //диалоговое окно для выбора цвета.

        public Form1()
        {
            InitializeComponent();
         //   label2.BackColor = Color.Black; //По умолчанию для пера задан черный цвет, поэтому мы зададим такой же фон для label2
            g = panel1.CreateGraphics(); //Создаем область для работы с графикой на элементе panel
        }

        private void label2_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK) //Если окно закрылось с OK, то меняем цвет для пера и фона label2
            {
                color = colorDialog.Color; //меняем цвет для пера
               // label2.BackColor = colorDialog.Color; //меняем цвет для Фона label2
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Flag = false;
            panel1.Refresh(); //Очищает элемент Panel
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            isPressed = false;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (isPressed)
            {
                PrevPoint = CurrentPoint;
                CurrentPoint = e.Location;
                my_Pen();
            }
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            isPressed = true;
            CurrentPoint = e.Location;
        }
        private void my_Pen() 
        {
         //   Pen pen = new Pen(color,(float)numericUpDown1.Value); //Создаем перо, задаем ему цвет и толщину.
           // g.DrawLine(pen, CurrentPoint, PrevPoint); //Соединияем точки линиями
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SolidBrush Float = new SolidBrush(Color.Yellow);
            SolidBrush Float2 = new SolidBrush(Color.Brown);
            SolidBrush window = new SolidBrush(Color.White);
            SolidBrush Home = new SolidBrush(Color.Gray);
            SolidBrush ground = new SolidBrush(Color.Green);
            SolidBrush backGround = new SolidBrush(Color.LightBlue);

            g.FillRectangle(Home, 350, 220, 200, 200);
            g.FillRectangle(Float2, 320, 190, 250, 50);
            g.FillRectangle(window, 400, 270, 90, 90);
            g.FillRectangle(Home, 360, 150, 35, 60);
        }


        float f1(float x)
        {           // функция графикс
            return 50-(float)(Math.Sin (x))*50;
        }

        private void panel2_Paint(object sender, PaintEventArgs e)
        {
            label4.Text = "50 - Sin(x) * 50";    // заголовок на окне
            Graphics cnt1 = Graphics.FromHwnd(panel2.Handle);
            Pen pen1 = new Pen(Color.BlueViolet, 2);
            cnt1.DrawLine(pen1, 0, 0, 200, 200);
            // Рисование графика
            float x1 = 0, y1 = f1(x1);
            while (x1 <= 150)
            {
                float x2 = x1 + 4;
                float y2 = f1(x2);
                cnt1.DrawLine(pen1, x1, y1, x2, y2);
                x1 = x2; y1 = y2;
            }
        }
        Graphics G;
        PointF[] Arr = new PointF[] // Исходный массив точек
        {
                new PointF(10,150),
                new PointF(5,50),
                new PointF(150,50),
                new PointF(140,140),
                new PointF(150,50),
                new PointF(150,50),
                new PointF(150,50),};
        int Fuctorial(int n) // Функция вычисления факториала
        {
            int res = 1;
            for (int i = 1; i <= n; i++)
                res *= i;
            return res;
        }
        float polinom(int i, int n, float t)// Функция вычисления полинома Бернштейна
        {
            return (Fuctorial(n) / (Fuctorial(i) * Fuctorial(n - i))) * (float)Math.Pow(t, i) * (float)Math.Pow(1 - t, n - i);
        }
        void Draw()// Функция рисования кривой
        {
            int j = 0;
            float step = 0.01f;// Возьмем шаг 0.01 для большей точности

            PointF[] result = new PointF[101];//Конечный массив точек кривой
            for (float t = 0; t < 1; t += step)
            {
                float ytmp = 0;
                float xtmp = 0;
                for (int i = 0; i < Arr.Length; i++)
                { // проходим по каждой точке
                    float b = polinom(i, Arr.Length - 1, t); // вычисляем наш полином Бернштейна
                    xtmp += Arr[i].X * b; // записываем и прибавляем результат
                    ytmp += Arr[i].Y * b;
                }
                result[j] = new PointF(xtmp, ytmp);
                j++;

            }
            G.DrawLines(new Pen(Color.Red), result);// Рисуем полученную кривую Безье
        }
        private void panel3_Paint(object sender, PaintEventArgs e)
        {
            G = Graphics.FromHwnd(panel3.Handle);
            G.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            Draw();
            label1.Text = "Кривая Безье";
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            SolidBrush Float = new SolidBrush(Color.Yellow);
            SolidBrush Float2 = new SolidBrush(Color.Brown);
            SolidBrush window = new SolidBrush(Color.Blue);
            SolidBrush Home = new SolidBrush(Color.Gray);
            SolidBrush ground = new SolidBrush(Color.Green);
            SolidBrush backGround = new SolidBrush(Color.LightBlue);
            g.FillRectangle(backGround, 0, 0, 10000, 10000);
            g.FillRectangle(ground, 0, 350, 10000, 10000);
            g.FillRectangle(Float2, 130, 220, 35, 145);
            g.FillEllipse(ground, 80, 150, 130, 130);
        }


    }
}
