// src/components/ShoppingCartItem.js

import React from "react";

const ShoppingCartItem = ({ item, onAdd, onRemove, onDelete }) => {
  return (
    <div className="shopping-cart-item">
      <div className="item-details">
        <img src={item.image} alt={item.name} className="item-image" />
        <div>
          <h4>{item.name}</h4>
          <p>{item.description}</p>
        </div>
      </div>
      <div className="item-quantity">
        <button onClick={() => onRemove(item.id)}>-</button>
        <span>{item.quantity}</span>
        <button onClick={() => onAdd(item.id)}>+</button>
      </div>
      <div className="item-price">$ {item.price * item.quantity}</div>
      <button onClick={() => onDelete(item.id)} className="delete-button">
        x
      </button>
    </div>
  );
};

export default ShoppingCartItem;
