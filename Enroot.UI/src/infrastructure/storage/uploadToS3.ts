import AWS from "aws-sdk";
import { useState } from "react";
import { v4 as uuidv4 } from "uuid";

type S3FileUploadResponse = {
  url: string;
};

const useS3FileUpload = (): [
  (file: File) => Promise<S3FileUploadResponse>,
  number
] => {
  const [progress, setProgress] = useState<number>(0);

  const uploadFileToS3 = (file: File): Promise<S3FileUploadResponse> => {
    const s3 = new AWS.S3({
      accessKeyId: "00302d7ff94c33b0000000007",
      secretAccessKey: "K0030AAf6+DEaTFeBPwERNPJqQube9M",
      endpoint: "https://s3.eu-central-003.backblazeb2.com",
    });

    const params = {
      Bucket: "EnrootAttachments",
      Key: uuidv4(),
      Body: file,
      ContentType: file.type,
    };

    const options = {
      partSize: 10 * 1024 * 1024, // 10 MB
      queueSize: 1,
    };

    const managedUpload = s3.upload(params, options);

    managedUpload.on("httpUploadProgress", (event) => {
      const percent = (event.loaded / event.total) * 100;
      setProgress(parseInt(percent.toFixed(2)));
    });

    return new Promise<S3FileUploadResponse>((resolve, reject) => {
      managedUpload.send((error, data) => {
        if (error) {
          reject(error);
        } else {
          resolve({ url: data.Location });
        }
      });
    });
  };

  const handleFileUpload = async (
    file: File
  ): Promise<S3FileUploadResponse> => {
    try {
      const resultUrl = await uploadFileToS3(file);
      return resultUrl;
    } catch (error) {
      console.error(error);
      throw error;
    }
  };

  return [handleFileUpload, progress];
};

export default useS3FileUpload;
