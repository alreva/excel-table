import { useForm } from 'react-hook-form';
import { useDropzone } from 'react-dropzone';
import { useCallback, useState } from 'react';
import axios from 'axios';

export default function UploadPage() {
  const [file, setFile] = useState<File | null>(null);
  const { handleSubmit } = useForm();

  const onDrop = useCallback((acceptedFiles: File[]) => {
    setFile(acceptedFiles[0]);
  }, []);

  const { getRootProps, getInputProps } = useDropzone({ onDrop });

  const uploadFile = async () => {
    if (!file) return;

    const formData = new FormData();
    formData.append('file', file);

    await axios.post('/api/file', formData, {
      headers: { 'Content-Type': 'multipart/form-data' },
    });

    alert('Upload successful!');
  };

  return (
    <form onSubmit={handleSubmit(uploadFile)} className="p-4">
      <div
        {...getRootProps()}
        className="border border-dashed rounded p-6 text-center cursor-pointer"
      >
        <input {...getInputProps()} />
        {file ? <p>{file.name}</p> : <p>Drag & drop Excel file here or click to select</p>}
      </div>
      <input type="submit" className="mt-4" value={'Upload'} />
    </form>
  );
}
