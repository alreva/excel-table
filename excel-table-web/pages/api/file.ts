import type { NextApiRequest, NextApiResponse } from 'next';
import * as formidable from 'formidable';
import fs from 'fs';
import FormData from 'form-data';
import axios from 'axios';

export const config = {
  api: { bodyParser: false }, // required for formidable
};

export default async function handler(req: NextApiRequest, res: NextApiResponse) {
  if (req.method !== 'POST') return res.status(405).end();

  const form = new formidable.IncomingForm();

  let fields;
  let files;
  try {
    [fields, files] = await form.parse(req);
  } catch (err:any) {
    if (err) return res.status(500).json({ error: 'Upload error' });
    // example to check for a very specific error
    if (err.code === formidable.errors.maxFieldsExceeded) {
      // ignore
    }
    console.error(err);
    res.writeHead(err.httpCode || 400, { 'Content-Type': 'text/plain' });
    res.end(String(err));
    return;
  }

  const file = files.file![0];
  const fileStream = fs.createReadStream(file.filepath);
  const formData = new FormData();
  formData.append('file', fileStream, file.originalFilename || 'uploaded.xlsx');

  try {
    const response = await axios.post(`${process.env.BACKEND_API_URL}/file`, formData, {
      headers: formData.getHeaders(),
    });

    res.status(response.status).json(response.data);
  } catch (error) {
    console.error('Error proxying request:', error);
    res.status(500).json({ error: 'Internal Server Error' });
  }
}
