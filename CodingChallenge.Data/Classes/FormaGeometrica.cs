/*
 * Refactorear la clase para respetar principios de programación orientada a objetos. Qué pasa si debemos soportar un nuevo idioma para los reportes, o
 * agregar más formas geométricas?
 *
 * Se puede hacer cualquier cambio que se crea necesario tanto en el código como en los tests. La única condición es que los tests pasen OK.
 *
 * TODO: Implementar Trapecio/Rectangulo, agregar otro idioma a reporting.
 * */

using CodingChallenge.Data.Negocio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public enum Formas
{
    Cuadrado = 1,
    TrianguloEquilatero = 2,
    Circulo = 3,
    Trapecio = 4,
    Rectangulo = 5
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


        //public Formas Tipo { get; set; }

        //public FormaGeometrica(Formas tipo, decimal ancho)
        //{
        //    Tipo = tipo;
        //    _lado = ancho;
        //}

        public static string Imprimir(List<FormasGeometricas> formas, Idiomas idioma)
        {
            StringBuilder sb;

            if (!formas.Any())
            {
                sb = FuncionesGenerales.AsignarIdiomaVacio(idioma);
            }
            else
            {
                // Hay por lo menos una forma
                // HEADER
                sb = FuncionesGenerales.AsignarIdioma(idioma);

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
                        case EnumFormas.Cuadrado:
                            numeroCuadrados++;                          
                            //areaCuadrados += formas[i].CalcularArea();
                            areaCuadrados += new Cuadrado(formas[i].Ancho).CalcularArea();
                            //perimetroCuadrados += formas[i].CalcularPerimetro();
                            perimetroCuadrados += new Cuadrado(formas[i].Ancho).CalcularPerimetro();
                            break;
                        case EnumFormas.Circulo:
                            numeroCirculos++;
                            areaCirculos += new Circulo(formas[i].Ancho).CalcularArea();
                            perimetroCirculos += new Circulo(formas[i].Ancho).CalcularPerimetro();
                            break;
                        case EnumFormas.TrianguloEquilatero:
                            numeroTriangulos++;
                            areaTriangulos += new TrianguloEquilatero(formas[i].Ancho).CalcularArea();
                            perimetroTriangulos += new TrianguloEquilatero(formas[i].Ancho).CalcularPerimetro();
                            break;
                        case EnumFormas.Rectangulo:
                            numeroTriangulos++;
                            areaTriangulos += new Rectangulo(formas[i].Ancho, formas[i].Largo).CalcularArea();
                            perimetroTriangulos += new Rectangulo(formas[i].Ancho, formas[i].Largo).CalcularPerimetro();
                            break;
                        case EnumFormas.Trapecio:
                            numeroTriangulos++;
                            areaTriangulos += new Trapecio(formas[i].Ancho, formas[i].Largo, formas[i].Altura).CalcularArea();
                            perimetroTriangulos += new Trapecio(formas[i].Ancho, formas[i].Largo, formas[i].Lado3, formas[i].Lado4).CalcularPerimetro();
                            break;
                    }
                }

                sb.Append(FuncionesGenerales.ObtenerLinea(numeroCuadrados, areaCuadrados, perimetroCuadrados, EnumFormas.Cuadrado, idioma));
                sb.Append(FuncionesGenerales.ObtenerLinea(numeroCirculos, areaCirculos, perimetroCirculos, EnumFormas.Circulo, idioma));
                sb.Append(FuncionesGenerales.ObtenerLinea(numeroTriangulos, areaTriangulos, perimetroTriangulos, EnumFormas.TrianguloEquilatero, idioma));

                // FOOTER
                sb.Append("TOTAL:<br/>");
                sb.Append(numeroCuadrados + numeroCirculos + numeroTriangulos + " " + (idioma == Idiomas.Castellano ? "formas" : "shapes") + " ");
                sb.Append((idioma == Idiomas.Castellano ? "Perimetro " : "Perimeter ") + (perimetroCuadrados + perimetroTriangulos + perimetroCirculos).ToString("#.##") + " ");
                sb.Append("Area " + (areaCuadrados + areaCirculos + areaTriangulos).ToString("#.##"));
            }

            return sb.ToString();
        }

    }
}
