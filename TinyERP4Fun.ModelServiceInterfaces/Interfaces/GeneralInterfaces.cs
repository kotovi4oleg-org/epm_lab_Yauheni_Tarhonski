using Microsoft.AspNetCore.Identity;


namespace TinyERP4Fun.Interfaces
{
    public interface IHaveName
    {
        string Name { get; }
    }
    public interface ICanSetName
    {
        string Name { get; set; }
    }
    public interface IHaveUser
    {
        string UserId { get; set; }
        IdentityUser User { get; set; }
    }
    public interface IHaveImage
    {
        byte[] Image { get; set; } // ссылка на изображение
        string ContentType { get; set; } // тип изображения
    }
    public interface IHaveLongId
    {
        long Id { get; set; }
    }
}
