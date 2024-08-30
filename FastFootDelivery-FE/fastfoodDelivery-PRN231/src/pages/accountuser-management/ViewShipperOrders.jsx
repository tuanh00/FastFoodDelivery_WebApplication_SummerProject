import React, { useEffect } from "react";
import { useDispatch, useSelector } from "react-redux";
import {
  fetchShipperOrders,
  updateOrderDeliveryStatus,
} from "../../redux/features/ViewShipperOrders";
import { Button, Modal } from "antd";
import axios from "axios";
import "bootstrap/dist/css/bootstrap.min.css";

const ViewShipperOrders = () => {
  const dispatch = useDispatch();
  const { orders, status, error } = useSelector((state) => state.shipperOrders);
  const ShipperId = useSelector((state) => state.accountmanage?.UserId);

  useEffect(() => {
    if (ShipperId) {
      dispatch(fetchShipperOrders(ShipperId));
    }
  }, [dispatch, ShipperId]);

  const confirmAction = (orderId, action) => {
    Modal.confirm({
      title: `Are you sure you want to ${
        action === "confirm" ? "confirm" : "cancel"
      } this order?`,
      onOk() {
        action === "confirm"
          ? handleConfirmOrder(orderId)
          : handleCancelOrder(orderId);
      },
    });
  };

  const handleConfirmOrder = async (orderId) => {
    try {
      const response = await axios.delete(
        `https://localhost:7173/api/Orders/GetConfirmOrderByShipper/${orderId}`
      );
      if (response.status === 200) {
        dispatch(updateOrderDeliveryStatus({ orderId, status: "Delivered" }));
        dispatch(fetchShipperOrders(ShipperId));
      }
    } catch (error) {
      console.error("Error confirming order:", error);
    }
  };

  const handleCancelOrder = async (orderId) => {
    try {
      const response = await axios.delete(
        `https://localhost:7173/api/Orders/GetCancelOrderByShipper/${orderId}`
      );
      if (response.status === 200) {
        dispatch(updateOrderDeliveryStatus({ orderId, status: "Cancelled" }));
        dispatch(fetchShipperOrders(ShipperId));
      }
    } catch (error) {
      console.error("Error canceling order:", error);
    }
  };

  if (status === "loading") {
    return <div>Loading...</div>;
  }

  if (status === "failed") {
    return <div>{error}</div>;
  }

  if (!orders.length) {
    return (
      <div className="mt-20">
        <h2>Shipper Orders</h2>
        <p>No orders to view.</p>
      </div>
    );
  }

  return (
    <div className=" mt-24">
      <h2>Shipper Orders</h2>
      <table className="table">
        <thead>
          <tr>
            <th>Order ID</th>
            <th>Required Date</th>
            <th>MemberName</th>
            <th>PhoneNumber</th>
            <th>Address</th>
            <th>TotalPrice</th>
            <th>Status Order</th>
            <th>Delivery Status</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {orders.map((order) => (
            <tr key={order.orderId}>
              <td>{order.orderId}</td>
              <td>{order.requiredDate}</td>
              <td>{order.memberName}</td>
              <td>{order.phoneNumber}</td>
              <td>{order.address}</td>
              <td>{order.totalPrice}</td>
              <td>{order.statusOrder}</td>
              <td>{order.deliveryStatus ?? "NULL"}</td>
              <td>
                <Button
                  className="btn btn-success me-2"
                  onClick={() => confirmAction(order.orderId, "confirm")}
                  disabled={
                    order.deliveryStatus && order.deliveryStatus !== "InTransit"
                  }
                >
                  Confirm
                </Button>
                <Button
                  className="btn btn-danger"
                  onClick={() => confirmAction(order.orderId, "cancel")}
                  disabled={
                    order.deliveryStatus && order.deliveryStatus !== "InTransit"
                  }
                >
                  Cancel
                </Button>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default ViewShipperOrders;
