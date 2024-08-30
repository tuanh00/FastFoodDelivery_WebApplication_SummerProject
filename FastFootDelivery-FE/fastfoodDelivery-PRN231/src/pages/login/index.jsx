import { async } from "@firebase/util";
import { Button, Form, Input } from "antd";
import { useForm } from "antd/es/form/Form";
import axios from "axios";
import { jwtDecode } from "jwt-decode";
import { useEffect, useState } from "react";
import { useDispatch, useSelector } from "react-redux";
import { Link, useNavigate } from "react-router-dom";
import { login } from "../../redux/features/userAccount";
import "./index.scss";
import { toast } from "react-toastify";

function Login() {
  const [formVariable] = useForm();
  const dispatch = useDispatch();
  const navigate = useNavigate();

  async function handleSubmit(values) {
    console.log(values);
    try {
      const response = await axios.post(
        "https://localhost:7173/api/User/login",
        values
      );

      const decoded = jwtDecode(response.data.data);
      dispatch(login(decoded));
      toast.success("login succefully");
      navigate("/");
    } catch (err) {
      toast.error("login fail");
      formVariable.resetFields();
    }
  }

  function handleOk() {
    formVariable.submit();
  }

  return (
    <div className="login">
      <img
        className="login__image"
        src="https://i.pinimg.com/originals/1a/04/9d/1a049d0db3c8a238ded1b4af745b1c1c.jpg"
        width="640"
        height="360"
        frameborder="0"
        allow="autoplay; fullscreen; picture-in-picture"
        allowfullscreen
      />

      <div className="wrapper">
        <div className="login__logo">
          <Link to="/">
            <img
              src="https://www.fastfoodcart.com/sites/default/files/logo_header_3/fastfoodcart2.png"
              alt=""
              width={200}
            />
          </Link>
        </div>
        <div className="line"></div>

        <div className="login__form">
          <h3>Login into your account</h3>

          <Form form={formVariable} onFinish={handleSubmit}>
            <Form.Item
              name="userName"
              rules={[
                {
                  required: true,
                  message: "Please Input username",
                },
              ]}
            >
              <Input type="text" placeholder="Username" />
            </Form.Item>
            <Form.Item
              name="password"
              rules={[
                {
                  required: true,
                  message: "Please Input password",
                },
              ]}
            >
              <Input type="password" placeholder="Password" />
            </Form.Item>
            <Form.Item>
              <Button onClick={handleOk}>Login</Button>
            </Form.Item>
          </Form>
          <div className="link">
            <Link to="/register">Register your Account</Link>
          </div>
        </div>
      </div>
    </div>
  );
}

export default Login;
