import { async } from "@firebase/util";
import { Modal, Table, Form, Input, Button, Space, Popconfirm } from "antd";
import { useForm } from "antd/es/form/Form";
import axios from "axios";
import React, { useEffect, useState } from "react";
import "./index.scss";
import { toast } from "react-toastify";

function Category() {
  const [formVariable] = useForm();
  const [formUpdate] = useForm();
  const [dataSource, setDataSource] = useState([]);
  const [isOpen, setIsOpen] = useState(false);
  const [visibleEditModal, setVisibleEditModal] = useState(false);
  const [idCategory, SetidCategory] = useState("");

  const handleDeleteCategory = async (categoryId) => {
    console.log(categoryId);

    await axios.delete(
      `https://localhost:7173/api/Category/DeleteCategory/${categoryId}`
    );

    const listAfterDelete = dataSource.filter(
      (category) => category.categoryId != categoryId
    );

    setDataSource(listAfterDelete);
    toast.success("Delete Category Successfully");
  };
  const columns = [
    {
      title: "categoriesName",
      dataIndex: "categoriesName",
      key: "categoriesName",
    },
    {
      title: "Action",
      dataIndex: "categoryId",
      key: "categoryId",
      render: (categoryId, data) => (
        <Space>
          <Popconfirm
            title="Delete Category"
            description="Are you sure to delete this Category?"
            onConfirm={() => handleDeleteCategory(categoryId)}
            onText="Yes"
            cancelText="No"
          >
            <Button type="primary" danger>
              Delete
            </Button>
          </Popconfirm>
          <Button
            onClick={() => {
              setVisibleEditModal(true);
              SetidCategory(categoryId);
              formUpdate.setFieldsValue(data);
            }}
            type="primary"
            style={{ background: "orange" }}
          >
            Update
          </Button>
        </Space>
      ),
    },
  ];

  function handleShowModal() {
    setIsOpen(true);
  }

  console.log(idCategory);

  function handleHideModal() {
    setIsOpen(false);
  }

  async function handleSubmit(values) {
    console.log(values);

    const response = await axios.post(
      "https://localhost:7173/api/Category/CreateCategory",
      values
    );

    setDataSource([...dataSource, values]);

    formVariable.resetFields();

    handleHideModal();
  }

  function handleOk() {
    formVariable.submit();
  }

  async function fetchCategory() {
    const response = await axios.get(
      "https://localhost:7173/api/Category/ViewAllCategorys"
    );
    setDataSource(response.data.data);
  }

  useEffect(() => {
    fetchCategory();
  }, []);

  function handleEditHideModal() {
    setVisibleEditModal(false);
  }

  async function handleEditCategory(value) {
    const updateCategory = formUpdate.getFieldsValue();
    console.log(updateCategory);
    axios
      .put(`https://localhost:7173/api/Category/UpdateCategory/${idCategory}`, {
        categoriesName: value.categoriesName,
      })
      .then(() => {
        fetchCategory();
        setVisibleEditModal(false);
        toast.success("Update Category Successfully");
      });
  }

  return (
    <div className="categoryPage">
      <Button type="primary" onClick={handleShowModal}>
        Add New Category
      </Button>
      <Table columns={columns} dataSource={dataSource}></Table>
      <Modal
        open={isOpen}
        title="Add New Category"
        onCancel={handleHideModal}
        onOk={handleOk}
      >
        <Form form={formVariable} onFinish={handleSubmit}>
          <Form.Item
            label="Category Name"
            name={"categoriesName"}
            rules={[
              {
                required: true,
                message: "Please Input Category Name",
              },
            ]}
          >
            <Input />
          </Form.Item>
        </Form>
      </Modal>

      <Modal
        open={visibleEditModal}
        title="Edit Category"
        onCancel={() => {
          setVisibleEditModal(false);
          formUpdate.resetFields();
        }}
        onOk={() => {
          formUpdate.submit();
          handleEditCategory();
        }}
      >
        <Form form={formUpdate} onFinish={handleEditCategory}>
          <Form.Item
            label="Category Name"
            name={"categoriesName"}
            rules={[
              {
                required: true,
                message: "Please Input Category Name",
              },
            ]}
          >
            <Input />
          </Form.Item>
        </Form>
      </Modal>
    </div>
  );
}

export default Category;
