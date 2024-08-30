import { Button, Form, Modal, Select, Table } from "antd";
import { useForm } from "antd/es/form/Form";
import axios from "axios";
import { Input } from "postcss";
import { useEffect, useState } from "react";
import { toast } from "react-toastify";

function OrderAdmin() {
  const [shipper, setShipper] = useState([]);
  const [idOder, setIdOder] = useState("");
  const fetchShipper = async () => {
    const reponse = await axios.get(`https://localhost:7173/api/Shipper`);

    const data = reponse.data.data;
    console.log({ data });
    const list = data.map((shipper, index) => ({
      value: shipper.id,
      label: <span>{shipper.fullName}</span>,
    }));
    setShipper(list);
  };
  useEffect(() => {
    fetchShipper();
  }, []);

  const [formVariable] = useForm();
  const [isOpen, setIsOpen] = useState(false);
  const [datasource, setDataSource] = useState([]);
  const columns = [
    {
      title: "orderDate",
      dataIndex: "orderDate",
      key: "orderDate",
    },
    {
      title: "shippedDate",
      dataIndex: "shippedDate",
      key: "shippedDate",
    },
    {
      title: "address",
      dataIndex: "address",
      key: "address",
    },
    {
      title: "totalPrice",
      dataIndex: "totalPrice",
      key: "totalPrice",
    },
    {
      title: "statusOrder",
      dataIndex: "statusOrder",
      key: "statusOrder",
    },
    {
      title: "deliveryStatus",
      dataIndex: "deliveryStatus",
      key: "deliveryStatus",
    },
    ,
    {
      title: "Action",
      dataIndex: "orderId",
      key: "orderId",
      render: (orderId, data) => (
        <Button
          onClick={() => {
            setIdOder(data.orderId);

            handleShowModal();
          }}
          type="primary"
          style={{ background: "orange" }}
          disabled={
            data.deliveryStatus !== "Processing" || data.statusOrder !== "Paid"
          }
        >
          Delivery
        </Button>
      ),
    },
  ];
  console.log(idOder);
  function handleShowModal() {
    setIsOpen(true);
  }

  function handleHideModal() {
    setIsOpen(false);
  }

  async function fetchOrder() {
    const response = await axios.get(
      "https://localhost:7173/api/Orders/ViewAllOrder"
    );
    setDataSource(response.data.data);
  }

  useEffect(() => {
    fetchOrder();
  }, []);

  async function handleEditShipper(value) {
    console.log(value.ShipperId);
    try {
      const response = await axios.put(
        `https://localhost:7173/api/Orders/UpdateOrderForShipper/${idOder}`,
        {
          shipperId: value.ShipperId,
        }
      );
      fetchOrder();
      formVariable.resetFields;
      handleHideModal();
      toast.success("Assign to the shipper Successfully");
    } catch (error) {
      toast.error("Assign Fail");
      console.log(e);
    }
  }
  return (
    <div>
      <Table columns={columns} dataSource={datasource}></Table>
      <Modal
        open={isOpen}
        Title="Add Shipper"
        onCancel={handleHideModal}
        onOk={() => formVariable.submit()}
      >
        <Form form={formVariable} onFinish={handleEditShipper}>
          <Form.Item
            label="Shipper"
            name="ShipperId"
            rules={[
              {
                required: true,
                message: "Please Input Shipper",
              },
            ]}
          >
            <Select options={shipper} />
          </Form.Item>
        </Form>
      </Modal>
    </div>
  );
}

export default OrderAdmin;
