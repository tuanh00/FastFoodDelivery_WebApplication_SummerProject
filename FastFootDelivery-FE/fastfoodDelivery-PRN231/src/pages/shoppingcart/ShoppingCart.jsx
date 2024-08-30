// src/components/ShoppingCart.js

import React, { useState } from "react";
import ShoppingCartItem from "./ShoppingCartItem";
import ShoppingCartSummary from "./ShoppingCartSummary";
import "./ShoppingCart.scss";
import { Link } from "react-router-dom";
import { useDispatch, useSelector } from "react-redux";
import {
  decreaseQuantity,
  increaseQuantity,
  removeAll,
  removeFood,
} from "../../redux/features/fastfoodCart";
import { store } from "../../redux/store";
import axios from "axios";
import { async } from "@firebase/util";
import { toast } from "react-toastify";

const ShoppingCart = () => {
  const initialItems = useSelector((state) => state.fastfoodcard);
  const userAccount = useSelector((state) => state.userAccount);
  const [items, setItems] = useState([]);
  const [dataSource, setDataSource] = useState([]);
  const [shipping, setShipping] = useState(5);
  const [oderId, setOderId] = useState([]);
  const dispatch = useDispatch();

  const cartData = useSelector((store) => store.fastfoodcard);
  const userData = useSelector((store) => store.accountmanage);
  console.log("cartData", cartData);
  console.log("userAccount", userData);
  console.log(userData);

  const handleSubmit = async () => {
    const orderDetails = initialItems.map((item) => ({
      foodId: item.id,
      quantity: item.quantity,
      price: item.price,
    }));

    const payload = {
      memberId: userData?.UserId, // fill this with appropriate memberId
      orderDate: new Date().toISOString(),
      shippedDate: new Date().toISOString(), // Corrected from ShipperDate to shippedDate
      requiredDate: new Date().toISOString(),
      address: "userData?.address", // fill this with appropriate address
      totalPrice: totalPrice,
      orderDetails: orderDetails,
    };

    console.log(payload);
    if (!userData?.UserId) {
      console.error("Error: memberId is missing");
      return;
    }
    try {
      const response = await axios.post(
        "https://localhost:7173/api/Orders/CreateOrder",
        payload
      );
      console.log("Order response", response.data.data);
      window.open(response.data.message);
    } catch (error) {
      console.error("Error creating order", error);
    }
  };

  // async function handleSubmit(values) {
  //   const response = await axios.post(
  //     "https://localhost:7173/api/Orders/CreateOrder",
  //    {
  //     {
  //       "memberId": userData.UserId,
  //       "orderDate": "2024-06-24T07:47:17.091Z",
  //       "shippedDate": "2024-06-24T07:47:17.091Z",
  //       "requiredDate": "2024-06-24T07:47:17.091Z",
  //       "address": "string",
  //       "totalPrice": 0,
  //       "orderDetails": cartData
  //     }
  //    }
  //   );
  //   setDataSource([...dataSource, values]);
  // }

  const handleAdd = (id) => {
    dispatch(increaseQuantity(id));
  };

  const handleDecrease = (id) => {
    dispatch(decreaseQuantity(id));
  };

  const handleDelete = (id) => {
    // setItems(items.filter((item) => item.id !== id));
    dispatch(removeFood(id));
  };

  const handleCheckout = () => {
    console.log("hi");

    handleSubmit();
    handleRemove();
  };

  const handleRemove = () => {
    dispatch(removeAll());
  };

  const totalPrice = initialItems?.reduce(
    (total, item) => total + item.price * item.quantity,
    0
  );

  return (
    <div className="shopping-cart">
      <div className="cart-items">
        <h2>Shopping Cart</h2>
        {initialItems?.map((item) => (
          <ShoppingCartItem
            key={item.id}
            item={item}
            onAdd={handleAdd}
            onRemove={handleDecrease}
            onDelete={handleDelete}
          />
        ))}
        <Link to="/">
          <button className="back-to-shop">Back to MenuFood</button>
        </Link>
      </div>
      <ShoppingCartSummary
        items={items}
        totalPrice={totalPrice}
        shipping={shipping}
        onCheckout={() => handleCheckout()}
      />
    </div>
  );
};

export default ShoppingCart;
