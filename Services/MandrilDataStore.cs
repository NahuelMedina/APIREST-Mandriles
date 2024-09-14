using MandrilAPI.Models;

namespace MandrilAPI.Services;

public class MandrilDataStore
{
    public List<Mandril> Mandriles { get; set; }

    public static MandrilDataStore Current { get; } = new MandrilDataStore();

    public MandrilDataStore()
    {
        Mandriles = new List<Mandril>() {
            new Mandril() {
                Id = 1,
                Nombre = "Mandril 1",
                Apellido = "Medina",
                Habilidades =  new List<Habilidad> () {
                new Habilidad() {
                    Id = 1,
                    Nombre = "Saltar",
                    Potencia = Habilidad.Epotencia.Moderado
                }
            } },
            new Mandril() {
                Id = 2,
                Nombre = "Mandril 2",
                Apellido = "Rodriguez",
                Habilidades =  new List<Habilidad> () {
                new Habilidad() {
                    Id = 1,
                    Nombre = "Saltar",
                    Potencia = Habilidad.Epotencia.Moderado
                },
                new Habilidad() {
                    Id = 2,
                    Nombre = "Caminar",
                    Potencia = Habilidad.Epotencia.Intenso
                },
                new Habilidad() {
                    Id = 3,
                    Nombre = "Gritar",
                    Potencia = Habilidad.Epotencia.Extremo
                }
            } },
            new Mandril() {
                Id = 3,
                Nombre = "Mandril 3",
                Apellido = "Colapinto",
                Habilidades =  new List<Habilidad> () {
                new Habilidad() {
                    Id = 1,
                    Nombre = "Saltar",
                    Potencia = Habilidad.Epotencia.Moderado
                },
                new Habilidad() {
                    Id = 2,
                    Nombre = "Nadar",
                    Potencia = Habilidad.Epotencia.Suave
                },
                new Habilidad() {
                    Id = 3,
                    Nombre = "Vomitar",
                    Potencia = Habilidad.Epotencia.Extremo
                }
            } },

        };
    }
}

