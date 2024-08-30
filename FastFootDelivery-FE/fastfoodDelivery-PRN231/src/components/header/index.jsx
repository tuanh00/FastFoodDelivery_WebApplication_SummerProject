import "./index.scss";
import { Link, useNavigate } from "react-router-dom";
import {
  SearchOutlined,
  UserOutlined,
  CloseOutlined,
  ShoppingCartOutlined,
  FundOutlined,
  ShoppingOutlined,
  TruckOutlined,
} from "@ant-design/icons";
import { useState } from "react";
import { Badge, Dropdown, Input } from "antd";
import { useDispatch, useSelector } from "react-redux";
import { logout } from "../../redux/features/userAccount";
import axios from "axios";

function Header({ setDataSearch, setCheck, setSearch }) {
  const [isShowSearch, setIsShowSearch] = useState(false);
  const value = useSelector((state) => state.fastfoodcard);
  const account = useSelector((store) => store.accountmanage);
  const dispatch = useDispatch();
  const navigate = useNavigate();
  const [searchTerm, setSearchTerm] = useState("");

  const handleButtonClick = (e) => {
    message.info("Click on left button.");
    console.log("click left button", e);
  };
  const handleMenuClick = ({ key }) => {
    if (key === "logout") {
      dispatch(logout());
      navigate("/login");
    }
    if (key === "profile") {
      navigate("/profolio");
    }
    if (key === "report") {
      navigate("/dashboard");
    }
    if (key == "vieworderhistory") {
      navigate("/viewOrderHistory");
    }
    if (key == "shipperpag") {
      navigate("/viewshipperOrders");
    }
  };
  const items = [
    {
      label: "ShipperPage",
      key: "shipperpag",
      icon: <TruckOutlined />,
    },
    {
      label: "OrderHistory",
      key: "vieworderhistory",
      icon: <ShoppingOutlined />,
    },
    {
      label: "report",
      key: "report",
      icon: <FundOutlined />,
    },
    {
      label: "Profile",
      key: "profile",
      icon: <UserOutlined />,
    },
    {
      label: "Logout",
      key: "logout",
      icon: <UserOutlined />,
    },
  ];

  const menuProps = {
    items,
    onClick: handleMenuClick,
  };

  const handleSearch = async (e) => {
    const term = e.target.value;
    setSearchTerm(term);
    if (term == "") {
      setCheck(true);
      setSearch(false);
    } else {
      setSearch(true);
    }
    const response = await axios.get(
      `https://localhost:7173/api/MenuItemFood/SearchFoods/search?searchTerm=${term}`
    );

    console.log(response.data.data);
    setDataSearch(response.data.data);

    // setSearchTerm(response.data.data);
  };
  return (
    <header className="header">
      <div className="header__logo">
        <Link to={"/"}>
          <img
            src="https://www.fastfoodcart.com/sites/default/files/logo_header_3/fastfoodcart2.png"
            alt=""
            width={100}
          />
        </Link>
      </div>

      <nav className="header__nav">
        <ul>
          <li>
            <Link to="/">Home</Link>
          </li>

          {/* <li className="foodManagement">
            FoodManagement
            <ul className="foodManagement__wrap">
              <li>
                <Link to="/fastfood-magegement">Fast Food</Link>
              </li>
              <li>
                <Link to="/category-management">Category</Link>
              </li>
            </ul>
          </li> */}
          {/* <li>
            <Link to="/accountuser-management">AccountManagement</Link>
          </li> */}

          {/* <li onClick={() => setIsShowSearch(true)}>
            <SearchOutlined />
          </li> */}
          <div className={`header__search ${isShowSearch ? "active" : ""}`}>
            <Input
              type={Text}
              placeholder="Search a fast food..."
              value={searchTerm}
              onChange={handleSearch}
            />
            {/* <CloseOutlined onClick={() => setIsShowSearch(false)} /> */}
          </div>
          <div className="search">
            <SearchOutlined
              className="search_icon"
              onClick={() => handleSearch()}
            />
          </div>
          <li>
            <Link to="/shoppingcart">
              <Badge count={value.length} showZero>
                <ShoppingCartOutlined />
              </Badge>
            </Link>
          </li>
          <li>
            {account ? (
              <Dropdown.Button menu={menuProps} onClick={handleButtonClick}>
                {account.name}
              </Dropdown.Button>
            ) : (
              <Link to="/login">
                <UserOutlined />
              </Link>
            )}
          </li>
        </ul>
      </nav>
    </header>
  );
}

export default Header;
