import { render, screen, fireEvent } from '@testing-library/react';
import App from '../App';

jest.mock('../hooks/useCommissionCalculator', () => ({
  useCommissionCalculator: jest.fn()
}));

import { useCommissionCalculator } from '../hooks/useCommissionCalculator';

describe('Commission Calculator UI', () => {
  const mockCalculate = jest.fn();

  beforeAll(() => {
    window.alert = jest.fn();
  });

  beforeEach(() => {
    useCommissionCalculator.mockReturnValue({
      results: {
        avalphaTechnologiesCommission: 0,
        competitorCommission: 0
      },
      isLoading: false,
      calculate: mockCalculate
    });
  });

  afterEach(() => {
    jest.clearAllMocks();
  });

  test('renders all input fields and button', () => {
    render(<App />);

    expect(screen.getByLabelText(/Local Sales Count/i)).toBeInTheDocument();
    expect(screen.getByLabelText(/Foreign Sales Count/i)).toBeInTheDocument();
    expect(screen.getByLabelText(/Average Sale Amount/i)).toBeInTheDocument();
    expect(
      screen.getByRole('button', { name: /Calculate Commission/i })
    ).toBeInTheDocument();
  });

  test('submits valid input and calls calculate()', () => {
    render(<App />);

    fireEvent.change(screen.getByLabelText(/Local Sales Count/i), {
      target: { value: '10' }
    });

    fireEvent.change(screen.getByLabelText(/Foreign Sales Count/i), {
      target: { value: '5' }
    });

    fireEvent.change(screen.getByLabelText(/Average Sale Amount/i), {
      target: { value: '100' }
    });

    fireEvent.click(
      screen.getByRole('button', { name: /Calculate Commission/i })
    );

    expect(mockCalculate).toHaveBeenCalledTimes(1);
    expect(mockCalculate).toHaveBeenCalledWith({
      localSalesCount: '10',
      foreignSalesCount: '5',
      averageSaleAmount: '100'
    });
  });

  test('does not submit when negative value is entered', () => {
    render(<App />);

    fireEvent.change(screen.getByLabelText(/Local Sales Count/i), {
      target: { value: '-1' }
    });

    fireEvent.click(
      screen.getByRole('button', { name: /Calculate Commission/i })
    );

    expect(mockCalculate).not.toHaveBeenCalled();
    expect(window.alert).toHaveBeenCalled();
  });

  test('shows results returned from hook', () => {
    useCommissionCalculator.mockReturnValue({
      results: {
        avalphaTechnologiesCommission: 700,
        competitorCommission: 137.75
      },
      isLoading: false,
      calculate: mockCalculate
    });

    render(<App />);

    expect(screen.getByText('£700')).toBeInTheDocument();
    expect(screen.getByText('£137.75')).toBeInTheDocument();
  });
});
