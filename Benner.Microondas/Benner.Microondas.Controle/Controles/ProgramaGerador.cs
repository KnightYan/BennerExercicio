using Benner.Microondas.Model.Dto;
using System.Collections.Generic;

namespace Benner.Microondas.Controle.Controles
{
    public class ProgramaGerador
    {
        public List<ProgramaDto> GerarProgramasPadroes()
        {
            var listaProgramas = new List<ProgramaDto>();

            listaProgramas.Add(new ProgramaDto
            {
                Nome = "Carnes Congeladas",
                CaractereAquecimento = "§",
                Descricao = "Para carnes congeladas",
                Compatibilidade = "Carnes, Frangos, Peixes",
                Tempo = 60,
                Potencia = 5
            });

            listaProgramas.Add(new ProgramaDto
            {
                Nome = "Pipoquinha",
                CaractereAquecimento = "¶",
                Descricao = "Para Pipocas",
                Compatibilidade = "Pipoca, Milho",
                Tempo = 60,
                Potencia = 1
            });

            listaProgramas.Add(new ProgramaDto
            {
                Nome = "Copo de Leite",
                CaractereAquecimento = "◄",
                Descricao = "Para esquentar o leite antes de dormir",
                Compatibilidade = "Leite, Agua",
                Tempo = 30,
                Potencia = 3
            });

            listaProgramas.Add(new ProgramaDto
            {
                Nome = "Lasanha",
                CaractereAquecimento = "█",
                Descricao = "Para Lasanhas",
                Compatibilidade = "Lasanhas",
                Tempo = 120,
                Potencia = 10
            });

            listaProgramas.Add(new ProgramaDto
            {
                Nome = "Sorvete",
                Descricao = "Para Soltar o sorvete duro do pote",
                CaractereAquecimento = "■",
                Compatibilidade = "Sorvetes",
                Tempo = 10,
                Potencia = 2
            });

            return listaProgramas;
        }
    }
}
