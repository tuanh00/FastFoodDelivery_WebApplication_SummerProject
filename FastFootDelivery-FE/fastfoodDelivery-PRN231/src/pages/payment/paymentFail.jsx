// PaymentFailure.js
import { Button } from "antd";
import React from "react";
import "./paymentFail.scss";

const PaymentFailure = () => {
  return (
    <div className="fail">
      <div className="iconfail">
        <div className="fail_icon">!</div>
      </div>
      <div className="message">Your payment failed</div>
      <div className="subMessage">Please try again</div>
      <button
        onClick={() => (window.location.href = "/")}
        className="payment-fail__button"
      >
        Go to Homepage
      </button>
    </div>
  );
};

export default PaymentFailure;
