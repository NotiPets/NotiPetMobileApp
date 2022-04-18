using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace NotiPetApp.Services
{
    public interface IDeviceUtils
    {
        Task<string> TakePhotoAsync();
    }
    public class DeviceUtils:IDeviceUtils
    {
       public async Task<string> TakePhotoAsync()
        {
            try
            {
                var photo = await MediaPicker.CapturePhotoAsync();
               var path = await LoadPhotoAsync(photo);
               return path; //Console.WriteLine($"CapturePhotoAsync COMPLETED: {PhotoPath}");
            }
            catch (FeatureNotSupportedException fnsEx)
            {
                // Feature is not supported on the device
            }
            catch (PermissionException pEx)
            {
                // Permissions not granted
            }
            catch (Exception ex)
            {
                Console.WriteLine($"CapturePhotoAsync THREW: {ex.Message}");
            }

            return string.Empty;
        }

       async Task<string> LoadPhotoAsync(FileResult photo)
       {
           Stream stream = null;
           // canceled
           if (photo == null)
           {
               return null;
           }

           var newFile = Path.Combine(FileSystem.CacheDirectory, photo.FileName);
           await using (stream = await photo.OpenReadAsync())
           {
               await using (var newStream = File.OpenWrite(newFile))
               {
                   await stream.CopyToAsync(newStream);
                   
               
               }
               return await GetFileOnBase64String(newFile);
           }
       }

        
        public async Task<string> GetFileOnBase64String(string path)
        {
            MemoryStream ms = new MemoryStream();
            await using FileStream fileStream = new FileStream(path, FileMode.Open, FileAccess.Read);
            await fileStream.CopyToAsync(ms);
            return Convert.ToBase64String(ms.ToArray());
        }
    }
}