using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

using SkiaSharp;
using SkiaSharp.Views.Forms;
using System.Diagnostics;
using Xamarin.Essentials;

namespace Farneti.Michele._4H.RiassuntoSkia
{
    public partial class MainPage : ContentPage
    {
        public int MARGINE_SINISTRO { get; set; } = 100;
        public int MARGINE_SOPRA { get; set; } = 100;
        public int LARGHEZZA_RETTANGOLO { get; set; } = 200;
        public int ALTEZZA_RETTANGOLO { get; set; } = 300;

        public SKPath rettangolo { get; set; }


        public MainPage()
        {
            InitializeComponent();
        }


        private void teladaDisegno_PaintSurface(object sender, SkiaSharp.Views.Forms.SKPaintSurfaceEventArgs e)
        {
            var info = e.Info;
            var canvas = e.Surface.Canvas;
            canvas.Clear();

            int larghezza = info.Rect.Width;  //1200
            int altezza = info.Rect.Width;    //794

            SKPaint paint = new SKPaint
            {
                Style = SKPaintStyle.Stroke,
                Color = SKColors.Black,
                StrokeWidth = 5
            };

            if(rettangolo!=null)
            {
                canvas.DrawPath(rettangolo, paint);
            }
            
        }

        private void btnDisegna_Clicked(object sender, EventArgs e)
        {
            rettangolo = new SKPath();
            disegnaRettangolo();
            disegnaDiagonali();

            teladaDisegno.InvalidateSurface();
        }

        private void disegnaRettangolo()
        {
            double xEDispositivo = MARGINE_SINISTRO;
            double yEDispositivo = MARGINE_SOPRA;
            double xFDispositivo = MARGINE_SINISTRO + LARGHEZZA_RETTANGOLO;
            double yFDispositivo = MARGINE_SOPRA;
            double xGDispositivo = MARGINE_SOPRA + LARGHEZZA_RETTANGOLO;
            double yGDispositivo = MARGINE_SOPRA + ALTEZZA_RETTANGOLO;
            double xHDispositivo = MARGINE_SINISTRO;
            double yHDispositivo = MARGINE_SOPRA + ALTEZZA_RETTANGOLO;


            disegnaSegmento(xEDispositivo, yEDispositivo, xFDispositivo, yFDispositivo);
            disegnaSegmento(xFDispositivo, yFDispositivo, xGDispositivo, yGDispositivo);
            disegnaSegmento(xGDispositivo, yGDispositivo, xHDispositivo, yHDispositivo);
            disegnaSegmento(xHDispositivo, yHDispositivo, xEDispositivo, yEDispositivo);
        }
        private void disegnaDiagonali()
        {
            double xFFoglio = 0;
            double yFFoglio = 0;
            double xHFoglio = 200;
            double yHFoglio = 300;

            //DIagonale 1
            disegnaSegmentoSulFoglio(xFFoglio, yFFoglio, xHFoglio, yHFoglio);
            //DIagonale 2
            disegnaSegmentoSulFoglio(xHFoglio, yFFoglio, xFFoglio, yHFoglio);
        }
        private void disegnaSegmento(double x1, double y1, double x2, double y2)
        {
            SKPoint p1 = new SKPoint((float)x1, (float)y1);
            SKPoint p2 = new SKPoint((float)x2, (float)y2);
            rettangolo.MoveTo(p1);
            rettangolo.LineTo(p2);
        }
        private void disegnaSegmentoSulFoglio(double x1, double y1, double x2, double y2)
        {
            double x1Dispositivo = trasformaXfoglio(x1);
            double y1Dispositivo = trasformaYfoglio(y1);
            double x2Dispositivo = trasformaXfoglio(x2);
            double y2Dispositivo = trasformaYfoglio(y2);

            SKPoint p1 = new SKPoint((float)x1Dispositivo, (float)y1Dispositivo);
            SKPoint p2 = new SKPoint((float)x2Dispositivo, (float)y2Dispositivo);
            rettangolo.MoveTo(p1);
            rettangolo.LineTo(p2);
        }
        private double trasformaXfoglio(double x)
        {
            return x + MARGINE_SINISTRO;
        }
        private double trasformaYfoglio(double y)
        {
            return MARGINE_SOPRA + ALTEZZA_RETTANGOLO - y;
        }
    }
}
