namespace Benner.Microondas.Model.Dto
{
    public class ProgramaDto
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public string CaractereAquecimento { get; set; }
        public string Compatibilidade { get; set; }
        public int? Tempo { get; set; }
        public int? Potencia { get; set; }
    }
}
