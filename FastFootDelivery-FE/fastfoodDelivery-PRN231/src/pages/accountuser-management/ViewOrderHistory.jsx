import React, { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import {
  fetchOrderHistory,
  updateOrderStatus,
} from "../../redux/features/ViewOrderHistory";
import { Button, Modal, Form, Input } from "antd";
import axios from "axios";

const ViewOrderHistory = () => {
  const dispatch = useDispatch();
  const { orders, status, error } = useSelector((state) => state.orderHistory);
  const UserId = useSelector((state) => state.accountmanage?.UserId);
  const [feedbackStatus, setFeedbackStatus] = useState({}); // State to track feedback submission per order

  useEffect(() => {
    if (UserId) {
      dispatch(fetchOrderHistory(UserId));
      fetchFeedbackStatus(UserId);
    }
  }, [dispatch, UserId]);

  const fetchFeedbackStatus = async (userId) => {
    try {
      const response = await axios.get(
        `https://localhost:7173/api/FeedBacks/ViewAllFeedBackByUserID/${userId}`
      );
      const feedbackData = response.data.data;
      const feedbackStatusMap = feedbackData.reduce((acc, feedback) => {
        acc[feedback.orderId] = true;
        return acc;
      }, {});
      setFeedbackStatus(feedbackStatusMap);
    } catch (error) {
      console.error("Failed to fetch feedback status:", error);
    }
  };

  const handleConfirmOrder = async (orderId) => {
    Modal.confirm({
      title: "Confirm Order",
      content: "Are you sure you want to confirm this order as received?",
      onOk: async () => {
        try {
          const response = await axios.delete(
            `https://localhost:7173/api/Orders/GetConfirmOrderByUser/${orderId}`
          );
          dispatch(updateOrderStatus({ orderId, status: "Received" }));
          dispatch(fetchOrderHistory(UserId)); // Refresh order history
        } catch (error) {
          alert("Failed to confirm order: " + error.message);
        }
      },
    });
  };

  const handleLeaveComment = (orderId) => {
    Modal.confirm({
      title: "Leave a Comment",
      content: (
        <Form
          onFinish={async (values) => {
            try {
              const response = await axios.post(
                "https://localhost:7173/api/FeedBacks/CreateFeedBack",
                {
                  userId: UserId,
                  orderId: orderId,
                  commentMsg: values.comment,
                }
              );
              setFeedbackStatus((prev) => ({ ...prev, [orderId]: true })); // Set feedback status to true for this order
              Modal.destroyAll();
            } catch (error) {
              alert("Failed to post comment: " + error.message);
            }
          }}
        >
          <Form.Item
            name="comment"
            rules={[{ required: true, message: "Please input your comment!" }]}
          >
            <Input placeholder="Leave a comment" />
          </Form.Item>
          <Form.Item>
            <div className="d-flex justify-content-between">
              <Button type="primary" htmlType="submit" className="me-2">
                Submit
              </Button>
              <Button onClick={() => Modal.destroyAll()}>Cancel</Button>
            </div>
          </Form.Item>
        </Form>
      ),
      footer: null,
    });
  };

  const handleViewFeedback = async (orderId) => {
    const response = await axios.get(
      `https://localhost:7173/api/FeedBacks/ViewAllFeedBackByUserID/${UserId}`
    );
    const feedback = response.data.data.find(
      (feedback) => feedback.orderId === orderId
    );

    if (feedback) {
      Modal.info({
        title: "View Feedback",
        content: (
          <div>
            <p>{feedback.commentMsg}</p>
            <Button
              className="me-2 bg-orange-500"
              onClick={() => handleUpdateFeedback(feedback.feedBackId)}
            >
              Update
            </Button>
            <Button
              className="me-2 bg-red-600"
              onClick={() => handleDeleteFeedback(feedback.feedBackId, orderId)}
            >
              Delete
            </Button>
            <Button
              className="me-2 bg-slate-400"
              onClick={() => Modal.destroyAll()}
            >
              Close
            </Button>
          </div>
        ),
        footer: null,
      });
    } else {
      alert("No feedback found for this order.");
    }
  };

  const handleUpdateFeedback = async (feedbackId) => {
    Modal.confirm({
      title: "Update Feedback",
      content: (
        <Form
          onFinish={async (values) => {
            try {
              const response = await axios.put(
                `https://localhost:7173/api/FeedBacks/UpdateFeedBack/${feedbackId}`,
                {
                  commentMsg: values.comment,
                }
              );
              Modal.destroyAll();
            } catch (error) {
              alert("Failed to update feedback: " + error.message);
            }
          }}
        >
          <Form.Item
            name="comment"
            rules={[
              { required: true, message: "Please input your updated comment!" },
            ]}
          >
            <Input placeholder="Update your comment" />
          </Form.Item>
          <Form.Item>
            <div className="d-flex justify-content-between">
              <Button type="primary" htmlType="submit" className="me-2">
                Update
              </Button>
              <Button onClick={() => Modal.destroyAll()}>Cancel</Button>
            </div>
          </Form.Item>
        </Form>
      ),
      footer: null,
    });
  };

  const handleDeleteFeedback = async (feedbackId, orderId) => {
    Modal.confirm({
      title: "Delete Feedback",
      content: "Are you sure you want to delete this feedback?",
      onOk: async () => {
        try {
          const response = await axios.delete(
            `https://localhost:7173/api/FeedBacks/DeleteFeedBack/${feedbackId}`
          );
          setFeedbackStatus((prev) => ({ ...prev, [orderId]: false })); // Set feedback status to false for this order
          Modal.destroyAll();
        } catch (error) {
          alert("Failed to delete feedback: " + error.message);
        }
      },
    });
  };

  if (status === "loading") {
    return <div>Loading...</div>;
  }

  if (status === "failed") {
    return <div>{error}</div>;
  }

  if (!orders.length) {
    return (
      <div className="container mt-3">
        <h2>Order History</h2>
        <p>No orders to view.</p>
      </div>
    );
  }

  return (
    <div className="mt-24">
      <h2>Order History</h2>
      <table className="table">
        <thead>
          <tr>
            <th>Order ID</th>
            <th>Name</th>
            <th>Order Date</th>
            <th>Shipped Date</th>
            <th>Address</th>
            <th>Total Price</th>
            <th>Status Order</th>
            <th>Delivery Status</th>
            <th>Actions</th>
          </tr>
        </thead>
        <tbody>
          {orders.map((order) => (
            <tr key={order.orderId}>
              <td>{order.orderId}</td>
              <td>{order.memberName}</td>
              <td>{order.orderDate}</td>
              <td>{order.shippedDate}</td>
              <td>{order.address}</td>
              <td>{order.totalPrice}</td>
              <td>{order.statusOrder}</td>
              <td>{order.deliveryStatus ?? "NULL"}</td>
              <td>
                <div className="d-flex justify-content-around">
                  <Button
                    className="btn btn-primary btn-sm me-1"
                    onClick={() => handleConfirmOrder(order.orderId)}
                    disabled={order.deliveryStatus !== "Delivered"}
                  >
                    Confirm
                  </Button>
                  <Button
                    className="btn btn-secondary btn-sm me-1"
                    onClick={() => handleLeaveComment(order.orderId)}
                    disabled={
                      order.deliveryStatus !== "Received" ||
                      feedbackStatus[order.orderId]
                    }
                  >
                    Feedback
                  </Button>
                  <Button
                    className="btn btn-info btn-sm"
                    onClick={() => handleViewFeedback(order.orderId)}
                    disabled={!feedbackStatus[order.orderId]}
                  >
                    View Feedback
                  </Button>
                </div>
              </td>
            </tr>
          ))}
        </tbody>
      </table>
    </div>
  );
};

export default ViewOrderHistory;
