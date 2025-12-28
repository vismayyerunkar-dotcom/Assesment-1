import { useRef, useState } from 'react';
import { calculateCommission } from '../api/commissionApi';

export function useCommissionCalculator() {
  const [results, setResults] = useState({
    avalphaTechnologiesCommission: 0,
    competitorCommission: 0
  });

  const [isLoading, setIsLoading] = useState(false);
  const lastRequestRef = useRef(null);

  const calculate = async (payload) => {
    const normalizedPayload = {
      localSalesCount: Number(payload.localSalesCount),
      foreignSalesCount: Number(payload.foreignSalesCount),
      averageSaleAmount: Number(payload.averageSaleAmount)
    };

    const requestKey = JSON.stringify(normalizedPayload);

    if (lastRequestRef.current === requestKey) return;

    lastRequestRef.current = requestKey;
    setIsLoading(true);

    try {
      const data = await calculateCommission(normalizedPayload);

      setResults({
        avalphaTechnologiesCommission:
          data.avalphaTechnologiesCommissionAmount,
        competitorCommission:
          data.competitorCommissionAmount
      });
    } finally {
      setIsLoading(false);
    }
  };

  return { results, isLoading, calculate };
}
