import axios from "axios";
import React, { useEffect, useState } from "react";
import useGetParams from "../../hooks/useGetParams";
import "./paymentSuccess.scss"; // Importing the CSS file for styling

const PaymentSuccess = ({ transactionId, amount, date }) => {
  const [dataSource, setDataSource] = useState([]);
  const params = useGetParams();
  const orderId = params("vnp_OrderInfo");
  console.log(orderId);
  const handleGetOrderById = async () => {
    const response = await axios.get(
      `https://localhost:7173/api/Orders/ViewOrderByID/${orderId}`
    );

    console.log(response.data.data);
    setDataSource(response.data.data);
  };

  useEffect(() => {
    handleGetOrderById();
  }, []);
  return (
    <div className="payment-success">
      <div className="payment-success__icon">
        <svg
          xmlns="http://www.w3.org/2000/svg"
          viewBox="0 0 24 24"
          fill="green"
          width="48px"
          height="48px"
        >
          <path d="M0 0h24v24H0z" fill="none" />
          <path d="M12 0C5.37 0 0 5.37 0 12s5.37 12 12 12 12-5.37 12-12S18.63 0 12 0zm-1.73 18l-4.99-5 1.41-1.41 3.58 3.58 7.59-7.59 1.41 1.41-9 9z" />
        </svg>
      </div>
      <h1>Payment Successful</h1>
      <p>Your payment has been processed successfully.</p>
      <div className="payment-success__details">
        <p>
          <strong>ShippedDate:</strong> {dataSource?.shippedDate}
        </p>

        <p>
          <strong>OrderDate:</strong> {dataSource?.orderDate}
        </p>
        <p>
          <strong>Amount:</strong> {dataSource?.totalPrice}
        </p>
      </div>
      <button
        onClick={() => (window.location.href = "/")}
        className="payment-success__button"
      >
        Go to Homepage
      </button>
    </div>
  );
};

export default PaymentSuccess;
