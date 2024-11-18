namespace ClinicaDental.ApiService.DataBase.Models.MedicalRecords;

using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Illness
{
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int IllnessId { get; private set; }

    [Required]
    public string Name { get; set; }

    [Required]
    public string Description { get; set; }

    public ICollection<Medicine> Treatments { get; set; }
}
