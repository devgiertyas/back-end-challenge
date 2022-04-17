using System.ComponentModel;

namespace TodoAPI.Helpers
{
    public class EnumApp
    {

        public enum StatusTodo : int
        {
            [DescriptionAttribute("Aguardando")]
            Waiting = 1,
            [DescriptionAttribute("Em andamento")]
            InProgress = 2,
            [DescriptionAttribute("Pendência")]
            Pending = 3,
            [DescriptionAttribute("Finalizado")]
            Finished = 4,
            [DescriptionAttribute("Outros")]
            Others = 5,

        }
    }
}
