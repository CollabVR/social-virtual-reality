public class Skin {
    public string name;
    public string location;
    public string image;

    public Skin(string name, string location, string image) {
        this.name = name;
        this.location = location;
        this.image = image;
    }
}

public class AvatarSkin {
    public Skin[] avatarSkins = new Skin[] {
        new Skin("Skin 1", "Assets/Textures/Skin1.png", "https://example.com/skin1.png"),
        new Skin("Skin 2", "Assets/Textures/Skin2.png", "https://example.com/skin2.png"),
        new Skin("Skin 3", "Assets/Textures/Skin3.png", "https://example.com/skin3.png")
    };
}
