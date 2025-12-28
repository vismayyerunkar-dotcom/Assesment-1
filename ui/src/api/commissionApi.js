const API_BASE_URL =
  process.env.REACT_APP_API_BASE_URL || 'https://localhost:5000';

export async function calculateCommission(payload) {
  const response = await fetch(
    `${API_BASE_URL}/Commision/calculate`,
    {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json'
      },
      body: JSON.stringify(payload)
    }
  );

  if (!response.ok) {
    const errorText = await response.text();
    throw new Error(errorText || 'Failed to calculate commission');
  }

  return response.json();
}
