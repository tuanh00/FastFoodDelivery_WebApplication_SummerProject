import React, { useEffect, useState } from "react";
import axios from "axios";
import { Button, Modal, Table } from "antd";

const ViewAllFeedback = () => {
  const [orders, setOrders] = useState([]);
  const [loading, setLoading] = useState(true);

  // Fetch all orders on component mount
  const fetchOrders = async () => {
    try {
      const response = await axios.get(
        "https://localhost:7173/api/Orders/ViewAllOrder"
      );
      setOrders(response.data.data);
      setLoading(false);
    } catch (error) {
      console.error("Failed to fetch orders:", error);
      setLoading(false);
    }
  };

  const handleViewFeedback = async (orderId) => {
    try {
      const response = await axios.get(
        `https://localhost:7173/api/FeedBacks/ViewAllFeedBackByOrderID/${orderId}`
      );
      const feedbacks = response.data.data;

      if (feedbacks && feedbacks.length > 0) {
        Modal.info({
          title: "View Feedback",
          content: (
            <div>
              {feedbacks.map((feedback) => (
                <p key={feedback.feedBackId}>{feedback.commentMsg}</p>
              ))}
              <Button onClick={() => Modal.destroyAll()}>Close</Button>
            </div>
          ),
          footer: null,
        });
      } else {
        Modal.info({
          title: "View Feedback",
          content: (
            <div>
              <p>No feedback to view.</p>
              <Button onClick={() => Modal.destroyAll()}>Close</Button>
            </div>
          ),
          footer: null,
        });
      }
    } catch (error) {
      console.error("Failed to fetch feedback:", error);
      Modal.error({
        title: "Error",
        content: "Failed to fetch feedback.",
      });
    }
  };

  useEffect(() => {
    fetchOrders();
  }, []);

  const columns = [
    {
      title: "Order ID",
      dataIndex: "orderId",
      key: "orderId",
    },
    {
      title: "Member Name",
      dataIndex: "memberName",
      key: "memberName",
    },
    {
      title: "Phone Number",
      dataIndex: "phoneNumber",
      key: "phoneNumber",
    },
    {
      title: "Order Date",
      dataIndex: "orderDate",
      key: "orderDate",
    },
    {
      title: "Shipped Date",
      dataIndex: "shippedDate",
      key: "shippedDate",
    },
    {
      title: "Address",
      dataIndex: "address",
      key: "address",
    },
    {
      title: "Total Price",
      dataIndex: "totalPrice",
      key: "totalPrice",
    },
    {
      title: "Status Order",
      dataIndex: "statusOrder",
      key: "statusOrder",
    },
    {
      title: "Delivery Status",
      dataIndex: "deliveryStatus",
      key: "deliveryStatus",
    },
    {
      title: "Actions",
      key: "actions",
      render: (_, record) => (
        <Button
          className="bg-primary"
          onClick={() => handleViewFeedback(record.orderId)}
        >
          View Feedback
        </Button>
      ),
    },
  ];

  if (loading) {
    return <div>Loading...</div>;
  }

  return (
    <div className="container mt-3">
      <h2>All Feedback</h2>
      <Table columns={columns} dataSource={orders} rowKey="orderId" />
    </div>
  );
};

export default ViewAllFeedback;
