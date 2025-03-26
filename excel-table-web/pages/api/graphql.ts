import type { NextApiRequest, NextApiResponse } from 'next';

export default async function handler(
  req: NextApiRequest,
  res: NextApiResponse
): Promise<void> {

  console.log('Running GraphQL API...');

  // Construct the GraphQL endpoint from the environment variable
  const graphQLEndpoint = `${process.env.BACKEND_API_URL}/graphql`;

  try {
    // Proxy the request to the GraphQL endpoint
    const response = await fetch(graphQLEndpoint, {
      method: req.method,
      headers: [
        ['Content-Type', 'application/json'],
      ],
      body: req.body ? JSON.stringify(req.body) : null,
    });

    const data = await response.json();
    res.status(response.status).json(data);
  } catch (error) {
    console.error('Error proxying request:', error);
    res.status(500).json({ error: 'Internal Server Error' });
  }
}
