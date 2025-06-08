namespace Application.Shared.Email;

public record ImageResource(string Cid, string ImagePath);

public record EmailBody(
    string CshtmlPath, 
    object Data,
    List<ImageResource>? ImageResources);
