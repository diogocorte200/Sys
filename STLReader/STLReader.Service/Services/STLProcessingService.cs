using STLReader.Domain.Models;
using System;
using System.IO;

namespace STLReader.Domain.Services
{
    public class STLProcessingService : ISTLProcessingService
    {
        public STLFileResult Process(Stream stlStream)
        {
            if (stlStream == null)
                throw new ArgumentNullException(nameof(stlStream));

            var result = new STLFileResult();
            try
            {
                using (StreamReader reader = new StreamReader(stlStream))
                {
                    int trianglesCount = 0; // Contador de triângulos
                    double totalArea = 0; // Área total
                    string line;

                    while ((line = reader.ReadLine()) != null)
                    {
                        if (line.Trim().StartsWith("outer loop"))
                        {
                            trianglesCount++; // Incrementa o contador de triângulos

                            var v1 = ParseVertex(reader.ReadLine());
                            var v2 = ParseVertex(reader.ReadLine());
                            var v3 = ParseVertex(reader.ReadLine());

                            var area = CalculateTriangleArea(v1, v2, v3);
                            if (area < 0)
                                throw new InvalidOperationException("Vértices do triângulo inválidos levando a uma área negativa");

                            // Adiciona a área do triângulo à área total
                            totalArea += area;
                        }
                    }

                    result.TriangleNumbers = trianglesCount;
                    result.SurfaceArea = totalArea;
                }
            }
            catch (IOException e)
            {
                throw new Exception("Erro ao ler do stream STL", e);
            }

            return result;
        }

        // Analisa uma linha de vértice no formato "vertex x y z"
        private static (double, double, double) ParseVertex(string line)
        {
            var parts = line.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

            if (parts.Length < 4)
                throw new FormatException("O formato esperado da linha de vértice não foi atendido.");

            try
            {
                return (Convert.ToDouble(parts[1]), Convert.ToDouble(parts[2]), Convert.ToDouble(parts[3]));
            }
            catch (FormatException e)
            {
                throw new FormatException("Erro ao analisar as coordenadas do vértice.", e);
            }
        }

        // Calcula a área de um triângulo no espaço 3D usando o produto vetorial de dois vetores
        private static double CalculateTriangleArea((double, double, double) v1, (double, double, double) v2, (double, double, double) v3)
        {
            // Desempacotando as coordenadas dos vértices
            var (x1, y1, z1) = v1;
            var (x2, y2, z2) = v2;
            var (x3, y3, z3) = v3;

            // Calculando os vetores entre os vértices
            var (dx1, dy1, dz1) = (x2 - x1, y2 - y1, z2 - z1);
            var (dx2, dy2, dz2) = (x3 - x1, y3 - y1, z3 - z1);

            // Calculando o produto vetorial dos dois vetores
            var (crossX, crossY, crossZ) = (
                dy1 * dz2 - dz1 * dy2,
                dz1 * dx2 - dx1 * dz2,
                dx1 * dy2 - dy1 * dx2
            );

            // Calculando a área do triângulo
            var area = 0.5 * Math.Sqrt(crossX * crossX + crossY * crossY + crossZ * crossZ);
            return area;
        }
    }
}
