/*
 * Refactorear la clase para respetar principios de programación orientada a objetos. Qué pasa si debemos soportar un nuevo idioma para los reportes, o
 * agregar más formas geométricas?
 *
 * Se puede hacer cualquier cambio que se crea necesario tanto en el código como en los tests. La única condición es que los tests pasen OK.
 *
 * TODO: Implementar Trapecio/Rectangulo, agregar otro idioma a reporting.
 * */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public enum Formas
{

    Cuadrado = 1,
    TrianguloEquilatero = 2,
    Circulo = 3,
    Trapecio = 4
}

public enum Idiomas
{
    Castellano = 1,
    Ingles = 2
}


namespace CodingChallenge.Data.Classes
{
    public class FormaGeometrica
    {
        //#region Formas

        //public const int Cuadrado = 1;
        //public const int TrianguloEquilatero = 2;
        //public const int Circulo = 3;
        //public const int Trapecio = 4;

        //#endregion

        //#region Idiomas

        //public const int Castellano = 1;
        //public const int Ingles = 2;

        //#endregion

        private readonly decimal _lado;

        public Formas Tipo { get; set; }

        public FormaGeometrica(Formas tipo, decimal ancho)
        {
            Tipo = tipo;
            _lado = ancho;
        }

        public static string Imprimir(List<FormaGeometrica> formas, Idiomas idioma)
        {
            StringBuilder sb;

            if (!formas.Any())
            {
                sb = AsignarIdioma(idioma);
            }
            else
            {
                // Hay por lo menos una forma
                // HEADER
                sb = AsignarIdioma(idioma);

                var numeroCuadrados = 0;
                var numeroCirculos = 0;
                var numeroTriangulos = 0;

                var areaCuadrados = 0m;
                var areaCirculos = 0m;
                var areaTriangulos = 0m;

                var perimetroCuadrados = 0m;
                var perimetroCirculos = 0m;
                var perimetroTriangulos = 0m;


                for (var i = 0; i < formas.Count; i++)
                {
                    switch (formas[i].Tipo)
                    {
                        case Formas.Cuadrado:
                            numeroCuadrados++;
                            areaCuadrados += formas[i].CalcularArea();
                            perimetroCuadrados += formas[i].CalcularPerimetro();
                            break;
                        case Formas.Circulo:
                            numeroCirculos++;
                            areaCirculos += formas[i].CalcularArea();
                            perimetroCirculos += formas[i].CalcularPerimetro();
                            break;
                        case Formas.TrianguloEquilatero:
                            numeroTriangulos++;
                            areaTriangulos += formas[i].CalcularArea();
                            perimetroTriangulos += formas[i].CalcularPerimetro();
                            break;
                    }
                }

                sb.Append(ObtenerLinea(numeroCuadrados, areaCuadrados, perimetroCuadrados, Formas.Cuadrado, idioma));
                sb.Append(ObtenerLinea(numeroCirculos, areaCirculos, perimetroCirculos, Formas.Circulo, idioma));
                sb.Append(ObtenerLinea(numeroTriangulos, areaTriangulos, perimetroTriangulos, Formas.TrianguloEquilatero, idioma));

                // FOOTER
                sb.Append("TOTAL:<br/>");
                sb.Append(numeroCuadrados + numeroCirculos + numeroTriangulos + " " + (idioma == Idiomas.Castellano ? "formas" : "shapes") + " ");
                sb.Append((idioma == Idiomas.Castellano ? "Perimetro " : "Perimeter ") + (perimetroCuadrados + perimetroTriangulos + perimetroCirculos).ToString("#.##") + " ");
                sb.Append("Area " + (areaCuadrados + areaCirculos + areaTriangulos).ToString("#.##"));
            }

            return sb.ToString();
        }

        private static string ObtenerLinea(int cantidad, decimal area, decimal perimetro, Formas tipo, Idiomas idioma)
        {
            if (cantidad > 0)
            {
                if (idioma == Idiomas.Castellano)
                    return $"{cantidad} {TraducirForma(tipo, cantidad, idioma)} | Area {area:#.##} | Perimetro {perimetro:#.##} <br/>";

                return $"{cantidad} {TraducirForma(tipo, cantidad, idioma)} | Area {area:#.##} | Perimeter {perimetro:#.##} <br/>";
            }

            return string.Empty;
        }

        private static string TraducirForma(Formas tipo, int cantidad, Idiomas idioma)
        {
            switch (tipo)
            {
                case Formas.Cuadrado:
                    if (idioma == Idiomas.Castellano) return cantidad == 1 ? "Cuadrado" : "Cuadrados";
                    else return cantidad == 1 ? "Square" : "Squares";
                case Formas.Circulo:
                    if (idioma == Idiomas.Castellano) return cantidad == 1 ? "Círculo" : "Círculos";
                    else return cantidad == 1 ? "Circle" : "Circles";
                case Formas.TrianguloEquilatero:
                    if (idioma == Idiomas.Castellano) return cantidad == 1 ? "Triángulo" : "Triángulos";
                    else return cantidad == 1 ? "Triangle" : "Triangles";
            }

            return string.Empty;
        }

        public decimal CalcularArea()
        {
            switch (Tipo)
            {
                case Formas.Cuadrado: return _lado * _lado;
                case Formas.Circulo: return (decimal)Math.PI * (_lado / 2) * (_lado / 2);
                case Formas.TrianguloEquilatero: return ((decimal)Math.Sqrt(3) / 4) * _lado * _lado;
                default:
                    throw new ArgumentOutOfRangeException(@"Forma Zdesconocida");
            }
        }

        public decimal CalcularPerimetro()
        {
            switch (Tipo)
            {
                case Formas.Cuadrado: return _lado * 4;
                case Formas.Circulo: return (decimal)Math.PI * _lado;
                case Formas.TrianguloEquilatero: return _lado * 3;
                default:
                    throw new ArgumentOutOfRangeException(@"Forma desconocida");
            }
        }

        private static StringBuilder AsignarIdioma(Idiomas idioma)
        {
            var sb = new StringBuilder();

            switch (idioma)
            {
                case Idiomas.Castellano:
                    sb.Append("<h1>Lista vacía de formas!</h1>");
                    break;
                case Idiomas.Ingles:
                    sb.Append("<h1>Empty list of shapes!</h1>");
                    break;
                default: break;
            }

            return sb;
        }
    }
}
