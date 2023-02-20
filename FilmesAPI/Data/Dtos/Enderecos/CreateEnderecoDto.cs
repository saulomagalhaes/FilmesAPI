using System.ComponentModel.DataAnnotations;

namespace FilmesAPI.Data.Dtos.Enderecos
{
    public class CreateEnderecoDto
    {
        [Required(ErrorMessage = "O logradouro do endereço é obrigatório")]
        public string Logradouro { get; set; }
        [Required(ErrorMessage = "O número do endereço é obrigatório")]
        public int Numero { get; set; }
    }
}
