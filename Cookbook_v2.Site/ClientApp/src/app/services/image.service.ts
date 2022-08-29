import { HttpClient } from "@angular/common/http";
import { Injectable } from "@angular/core";
import { Observable } from "rxjs";

@Injectable({
  providedIn: "root",
})
export class ImageService {
  constructor() {}

  public async readImageAsBase64(file: any): Promise<string> {
    this.VerifyFileTypeAndSize(file);
    
    const promise = await new Promise((resolve, reject) => {
      const reader = new FileReader();
      reader.readAsDataURL(file);
      reader.onload = (rs) => {
        resolve(reader.result);
      };
      reader.onerror = () => reject;
    });

    const image = new Image();
    image.src = (await promise) as string;
    await image.decode();
    this.VerifyImageDimensions(image);

    return image.src.split(',')[1];
  }

  private VerifyFileTypeAndSize(file: any): void {
    const allowedFileTypes = ["image/jpeg", "image/png"];
    const maxSize: number = 5000000;

    if (!allowedFileTypes.includes(file.type)) {
      throw "Допустимы только изображения: JPG, PNG";
    }

    if (file.size > maxSize) {
      throw "Максимальный допустимый вес файла " + maxSize / 1000 + "Мб";
    }
  }

  private VerifyImageDimensions(image: any): void {
    const maxHeight: number = 2500;
    const maxWidth: number = 2500;

    if (image.width > maxWidth ||
        image.heigh > maxHeight) {
      throw ("Максимальный допустимый размер изображения " + 
        maxWidth + "x" + maxHeight + " пикселей");
    }
  }
}
