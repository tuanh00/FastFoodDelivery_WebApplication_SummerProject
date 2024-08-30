// src/components/ShoppingCartSummary.js

import React from "react";

const ShoppingCartSummary = ({ items, totalPrice, shipping, onCheckout }) => {
  return (
    <div className="shopping-cart-summary">
      <h3>Summary</h3>
      <div>
        <p>ITEMS {items?.length}</p>
        <p>$ {totalPrice}</p>
      </div>
      <div>
        <label>SHIPPING</label>
        <select>
          <option value="5">Standard-Delivery - $5.00</option>
          <option value="10">Express-Delivery - $10.00</option>
        </select>
      </div>
      <div>
        <label>GIVE CODE</label>
        <input type="text" placeholder="Enter your code" />
      </div>
      <div>
        <p>TOTAL PRICE</p>
        <p>$ {totalPrice + shipping}</p>
      </div>
      <button onClick={onCheckout}>CHECKOUT</button>
    </div>
  );
};

export default ShoppingCartSummary;
