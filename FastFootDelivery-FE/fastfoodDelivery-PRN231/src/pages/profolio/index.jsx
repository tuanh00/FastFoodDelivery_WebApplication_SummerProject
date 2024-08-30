import React, { useEffect, useState } from "react";
import {
  MDBCol,
  MDBContainer,
  MDBRow,
  MDBCard,
  MDBCardText,
  MDBCardBody,
  MDBCardImage,
  MDBBtn,
  MDBBreadcrumb,
  MDBBreadcrumbItem,
  MDBProgress,
  MDBProgressBar,
  MDBIcon,
  MDBListGroup,
  MDBListGroupItem,
  MDBTypography,
  MDBInput,
} from "mdb-react-ui-kit";
import { Button, Descriptions, Form, Input } from "antd";
import "./profolio.scss";
import { useSelector } from "react-redux";
import axios from "axios";
import Modal from "antd/es/modal/Modal";
import { async } from "@firebase/util";
import { useForm } from "antd/es/form/Form";

export default function ProfilePage() {
  const [user, setUser] = useState([]);
  const AccountUser = useSelector((store) => store.accountmanage);
  const [isOpen, setIsOpen] = useState(false);
  console.log(user);
  console.log(`${AccountUser.UserId}`);

  const fetchUserId = async () => {
    console.log("hi");
    const response = await axios.get(
      `https://localhost:7173/api/User/GetUserById/${AccountUser.UserId}`
    );
    console.log(response.data);
    setUser(response.data.data);
  };
  useEffect(() => {
    fetchUserId();
  }, []);
  if (!user) {
    return <div>Loading...</div>;
  }

  function handleShowModal() {
    setIsOpen(true);
  }
  function handleHileModal() {
    setIsOpen(false);
  }

  async function handleEditAccount() {
    const updateAccount = formUpdate.getFieldValue();
    await axios.put(
      `https://localhost:7173/api/User/UpdateUser?id=${AccountUser.UserId}`,
      updateAccount
    );
    fetchUserId(); // Refresh user data
    handleHileModal();
  }

  const [formUpdate] = useForm();
  return (
    <section style={{ backgroundColor: "#eee" }}>
      <MDBContainer className="py-5 border-none">
        <MDBRow>
          <MDBCol lg="4 ">
            <MDBCard className="mb-4 ">
              <MDBCardBody className="text-center flex flex-col items-center bg-orange-300">
                <MDBCardImage
                  src="https://mdbcdn.b-cdn.net/img/Photos/new-templates/bootstrap-chat/ava3.webp"
                  alt="avatar"
                  className="rounded-circle py-12"
                  style={{ width: "150px" }}
                  fluid
                />
              </MDBCardBody>
            </MDBCard>
          </MDBCol>
          <MDBCol lg="8">
            <MDBCard className="mb-4">
              <MDBCardBody>
                <MDBTypography tag="col-md-9" className="info">
                  Information
                </MDBTypography>
                <hr />
                <MDBRow>
                  <MDBCol sm="3">
                    <MDBCardText>Full Name</MDBCardText>
                  </MDBCol>
                  <MDBCol sm="9">
                    <MDBCardText className="text-muted">
                      {user.fullName}
                    </MDBCardText>
                  </MDBCol>
                </MDBRow>
                <hr />
                <MDBRow>
                  <MDBCol sm="3">
                    <MDBCardText>Email</MDBCardText>
                  </MDBCol>
                  <MDBCol sm="9">
                    <MDBCardText className="text-muted">
                      {user.email}
                    </MDBCardText>
                  </MDBCol>
                </MDBRow>
                <hr />
                <MDBRow>
                  <MDBCol sm="3">
                    <MDBCardText>PhoneNumber</MDBCardText>
                  </MDBCol>
                  <MDBCol sm="9">
                    <MDBCardText className="text-muted">
                      {user.phoneNumber}
                    </MDBCardText>
                  </MDBCol>
                </MDBRow>
                <hr />
                <MDBRow>
                  <MDBCol sm="3">
                    <MDBCardText>Address</MDBCardText>
                  </MDBCol>
                  <MDBCol sm="9">
                    <MDBCardText className="text-muted">
                      {user.address}
                    </MDBCardText>
                  </MDBCol>
                </MDBRow>
              </MDBCardBody>
            </MDBCard>
            <div>
              <Button
                // className=" px-20 bg-orange-300"
                className="btn-edit"
                onClick={() => {
                  setIsOpen(true);
                  formUpdate.setFieldsValue(user);
                }}
              >
                Edit
              </Button>
              <Modal
                title="Edit Account Item"
                open={isOpen}
                onOk={handleEditAccount}
                onCancel={handleHileModal}
              >
                <Form form={formUpdate}>
                  <Form.Item label="FullName" name="fullName">
                    <Input />
                  </Form.Item>
                  <Form.Item label="Address" name="address">
                    <Input />
                  </Form.Item>
                  <Form.Item label="Email" name="email">
                    <Input />
                  </Form.Item>
                  <Form.Item label="PhoneNumber" name="phoneNumber">
                    <Input />
                  </Form.Item>
                </Form>
              </Modal>
            </div>
          </MDBCol>
        </MDBRow>
      </MDBContainer>
    </section>
  );
}
