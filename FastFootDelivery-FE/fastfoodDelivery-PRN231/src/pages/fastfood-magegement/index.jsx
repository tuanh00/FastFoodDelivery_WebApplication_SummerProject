import {
  Button,
  Form,
  Image,
  Input,
  Modal,
  Popconfirm,
  Select,
  Space,
  Table,
  Upload,
} from "antd";
import TextArea from "antd/es/input/TextArea";
import axios from "axios";
import { useEffect, useState } from "react";
import { PlusOutlined } from "@ant-design/icons";
import { useForm } from "antd/es/form/Form";
import uploadFile from "../../utils/upload";
import "./index.scss";
import { toast } from "react-toastify";

function FoodItemManagement() {
  const [category, setCategory] = useState([]);
  const fetchCategories = async () => {
    const resonse = await axios.get(
      `https://localhost:7173/api/Category/ViewAllCategorys`
    );
    const data = resonse.data.data;
    console.log({ data });
    const list = data.map((category, index) => ({
      value: category.categoryId,
      label: <span>{category.categoriesName}</span>,
    }));
    setCategory(list);
  };
  useEffect(() => {
    fetchCategories();
  }, []);
  console.log(category);
  const [formVariable] = useForm();

  const [dataSource, setDataSource] = useState([]);
  const [isOpen, setIsOpen] = useState(false);

  const [visibleEditModal, setVisibleEditModal] = useState(false);
  const [fastfoodEdit, setFastFoodEdit] = useState(null);
  const [oldFood, setOldFood] = useState({});

  const handleDeleteFastFood = async (foodId) => {
    console.log("Delete Fast Food", foodId);

    await axios.delete(
      `https://localhost:7173/api/MenuItemFood/DeleteFood/${foodId}`
    );

    const listAfterDelete = dataSource.filter(
      (fastfoot) => fastfoot.foodId != foodId
    );
    setDataSource(listAfterDelete);
    toast.success("Delete Food Successfully");
  };
  const columns = [
    {
      title: "foodName",
      dataIndex: "foodName",
      key: "foodName",
    },
    {
      title: "foodDescription",
      dataIndex: "foodDescription",
      key: "foodDescription",
    },
    {
      title: "unitPrice",
      dataIndex: "unitPrice",
      key: "unitPrice",
    },
    {
      title: "foodStatus",
      dataIndex: "foodStatus",
      key: "foodStatus",
    },
    {
      title: "Image",
      dataIndex: "image",
      key: "image",
      render: (image) => <Image src={image} width={300} />,
    },
    {
      title: "Action",
      dataIndex: "foodId",
      key: "foodId",
      render: (foodId, data) => (
        <Space>
          <Popconfirm
            title="Delete the task"
            description="Are you sure to delete this task?"
            onConfirm={() => handleDeleteFastFood(foodId)}
            okText="Yes"
            cancelText="No"
          >
            <Button type="primary" danger>
              Delete
            </Button>
          </Popconfirm>
          <Button
            onClick={() => {
              setVisibleEditModal(true);
              // formUpdate.setFieldsValue(data);
              handleOnEdit(data);
              // setOldFood(data);
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
  const [previewOpen, setPreviewOpen] = useState(false);
  const [previewImage, setPreviewImage] = useState("");
  const [fileList, setFileList] = useState([]);

  const getBase64 = (file) =>
    new Promise((resolve, reject) => {
      const reader = new FileReader();
      reader.readAsDataURL(file);
      reader.onload = () => resolve(reader.result);
      reader.onerror = (error) => reject(error);
    });

  const handlePreview = async (file) => {
    if (!file.url && !file.preview) {
      file.preview = await getBase64(file.originFileObj);
    }
    setPreviewImage(file.url || file.preview);
    setPreviewOpen(true);
  };
  const handleChange = ({ fileList: newFileList }) => setFileList(newFileList);
  const uploadButton = (
    <button
      style={{
        border: 0,
        background: "none",
      }}
      type="button"
    >
      <PlusOutlined />
      <div
        style={{
          marginTop: 8,
        }}
      >
        Upload
      </div>
    </button>
  );

  function handleShowModal() {
    setIsOpen(true);
  }

  function handleHideModal() {
    setIsOpen(false);
  }

  async function handleSubmit(values) {
    try {
      console.log(values);
      console.log(values.image.file);
      if (values.image?.file) {
        const url = await uploadFile(values.image.file);
        values.image = url;
        console.log(values);
      }

      const response = await axios.post(
        "https://localhost:7173/api/MenuItemFood/CreateFood",
        values
      );
      setDataSource([...dataSource, response.data.data]);

      // clear form
      formVariable.resetFields();

      //hide form
      handleHideModal();
      toast.success("Create Food Item successfully!");
    } catch (err) {
      toast.error("Create Food Item Fail");
    }
  }

  function handleOk() {
    formVariable.submit();
  }

  async function fetchFastFood() {
    const response = await axios.get(
      "https://localhost:7173/api/MenuItemFood/ViewAllFoods"
    );
    console.log("===============================>>>>>");
    console.log(response.data);
    setDataSource(response.data.data);
  }

  useEffect(() => {
    fetchFastFood();
  }, []);

  // //------------------update---------------------------
  function handleOpenEditModal() {
    setVisibleEditModal(true);
  }

  function handleCloseEditModal() {
    setVisibleEditModal(false);
  }

  function handleOnEdit(food) {
    console.log(food);
    setOldFood(food);
    handleOpenEditModal();
  }

  function handleResetEditing() {
    setOldFood(null);
    handleCloseEditModal();
  }

  async function handleEditCource() {
    // console.log(oldFood);
    // const response = axios.put(
    //   `https://localhost:7173/api/MenuItemFood/UpdateFood/${oldFood.foodId}`,
    //   {
    //     foodId: oldFood.foodId,
    //     foodName: oldFood.foodName,
    //     foodDescription: oldFood.foodDescription,
    //     unitPrice: oldFood.unitPrice,
    //     categoryId: oldFood.categoryId,
    //     image: oldFood.image,foodId: oldFood.foodId,

    //   }
    // );

    // fetchFastFood();
    // handleCloseEditModal();

    try {
      const updatedFood = formUpdate.getFieldsValue();
      let imageUrl = oldFood.image;

      if (fileList.length && fileList[0].originFileObj) {
        imageUrl = await uploadFile(fileList[0].originFileObj);
      }

      axios
        .put(
          `https://localhost:7173/api/MenuItemFood/UpdateFood/${oldFood.foodId}`,
          { ...updatedFood, foodId: oldFood.foodId, image: imageUrl }
        )
        .then(() => {
          fetchFastFood();
          setVisibleEditModal(false);
          toast.success("Update Food Item Successfully");
        });
    } catch (err) {
      toast.error("Update Food Item Fail");
    }
  }
  const [formUpdate] = Form.useForm();

  useEffect(() => {
    if (!oldFood) return;
    formUpdate.setFieldsValue(oldFood);
    setFileList([
      {
        uid: "-1",
        name: "image.png",
        status: "done",
        url: oldFood?.image,
      },
    ]);
  }, [oldFood]);
  //----------------------------------------------------

  console.log(oldFood);
  return (
    <div className="fastfoodpage">
      <Button type="primary" onClick={handleShowModal}>
        Add New Fast Food
      </Button>
      <Table columns={columns} dataSource={dataSource}></Table>
      <Modal
        open={isOpen}
        title="Add New Fast Food"
        onCancel={handleHideModal}
        onOk={handleOk}
      >
        <Form
          labelCol={{
            span: 24,
          }}
          form={formVariable}
          onFinish={handleSubmit}
        >
          <Form.Item
            label="Food name"
            name={"foodName"}
            rules={[
              {
                required: true,
                message: "Please Input Food name",
              },
            ]}
          >
            <Input />
          </Form.Item>
          <Form.Item
            label="Description"
            name={"foodDescription"}
            rules={[
              {
                required: true,
                message: "Please Input Description",
              },
            ]}
          >
            <TextArea rows={4} />
          </Form.Item>
          <Form.Item
            label="UnitPrice"
            name={"unitPrice"}
            rules={[
              {
                required: true,
                message: "Please Input UnitPrice",
              },
            ]}
          >
            <Input />
          </Form.Item>

          <Form.Item
            label="Category"
            name="categoryId"
            rules={[
              {
                required: true,
                message: "Please Input Category",
              },
            ]}
          >
            <Select options={category} />
          </Form.Item>

          <Form.Item
            label="Image"
            name={"image"}
            rules={[
              {
                required: true,
                message: "Please Input Image",
              },
            ]}
          >
            <Upload
              action="https://660d2bd96ddfa2943b33731c.mockapi.io/api/upload"
              listType="picture-card"
              fileList={fileList}
              onPreview={handlePreview}
              onChange={handleChange}
              beforeUpload={() => false}
            >
              {fileList.length >= 1 ? null : uploadButton}
            </Upload>
          </Form.Item>
        </Form>
      </Modal>
      {previewImage && (
        <Image
          wrapperStyle={{
            display: "none",
          }}
          preview={{
            visible: previewOpen,
            onVisibleChange: (visible) => setPreviewOpen(visible),
            afterOpenChange: (visible) => !visible && setPreviewImage(""),
          }}
          src={previewImage}
        />
      )}
      {/* //------------------UpDate---------------------------- */}
      <Modal
        title="Edit Foot Item"
        open={visibleEditModal}
        onOk={handleEditCource}
        onCancel={() => {
          setVisibleEditModal(false);
          // formUpdate.resetFields();
        }}
        okText={"Save"}
      >
        <Form form={formUpdate}>
          <Form.Item
            label="Food name"
            name={"foodName"}
            rules={[
              {
                required: true,
                message: "Please Input Food name",
              },
            ]}
          >
            <Input
              value={oldFood?.foodName}
              onChange={(e) => {
                const value = e;
                setFastFoodEdit((oldFood) => ({ ...oldFood, foodName: value }));
              }}
            />
          </Form.Item>
          <Form.Item
            label="Description"
            name={"foodDescription"}
            rules={[
              {
                required: true,
                message: "Please Input Description",
              },
            ]}
          >
            <TextArea
              rows={4}
              value={oldFood?.description}
              onChange={(e) => {
                const value = e;
                setFastFoodEdit((oldFood) => ({
                  ...oldFood,
                  description: value,
                }));
              }}
              // onChange={(e) => {
              //   console.log(oldFood);
              //   setFastFoodEdit((oldFood) => {
              //     return { ...oldFood, description: e };
              //   });
              // }}
            />
          </Form.Item>
          <Form.Item
            label="UnitPrice"
            name={"unitPrice"}
            rules={[
              {
                required: true,
                message: "Please Input UnitPrice",
              },
            ]}
          >
            <Input
              value={oldFood?.unitPrice}
              onChange={(e) => {
                const value = e;
                setFastFoodEdit((oldFood) => ({
                  ...oldFood,
                  unitPrice: value,
                }));
              }}
              // onChange={(e) => {
              //   console.log(oldFood);
              //   setFastFoodEdit((oldFood) => {
              //     return { ...oldFood, unitPrice: e };
              //   });
              // }}
            />
          </Form.Item>
          <Form.Item
            label="Category"
            name="categoryId"
            rules={[
              {
                required: true,
                message: "Please Input Category",
              },
            ]}
          >
            <Select
              value={oldFood?.categoryId}
              onChange={(e) => {
                const value = e;
                setFastFoodEdit((oldFood) => ({
                  ...oldFood,
                  categoryId: value,
                }));
              }}
              // onChange={(e) => {
              //   console.log(oldFood);
              //   setFastFoodEdit((oldFood) => {
              //     return { ...oldFood, categoryId: e };
              //   });
              // }}
              options={category}
            />
          </Form.Item>
          <Form.Item
            label="Image"
            name={"image"}
            rules={[
              {
                required: true,
                message: "Please Input Image",
              },
            ]}
          >
            <Upload
              action="https://660d2bd96ddfa2943b33731c.mockapi.io/api/upload"
              listType="picture-card"
              fileList={fileList}
              onPreview={handlePreview}
              onChange={handleChange}
            >
              {fileList.length >= 8 ? null : uploadButton}
              <Image
                wrapperStyle={{
                  display: "none",
                }}
                preview={{
                  visible: previewOpen,
                  onVisibleChange: (visible) => setPreviewOpen(visible),
                  afterOpenChange: (visible) => !visible && setPreviewImage(""),
                }}
                src={oldFood?.image}
                // onChange={(e) => {
                //   const value = e;
                //   setFastFoodEdit((oldFood) => ({
                //     ...oldFood,
                //     image: value,
                //   }));
                // }}
                // onChange={(e) => {
                //   console.log(oldFood);
                //   setFastFoodEdit((oldFood) => {
                //     return { ...oldFood, image: e };
                //   });
                // }}
              />
            </Upload>
          </Form.Item>
        </Form>
      </Modal>
    </div>
  );
}

export default FoodItemManagement;
