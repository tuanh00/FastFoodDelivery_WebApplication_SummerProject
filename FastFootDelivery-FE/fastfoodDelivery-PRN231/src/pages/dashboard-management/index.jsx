import { useEffect, useState } from "react";
import {
  ProfileOutlined,
  HeartOutlined,
  UserOutlined,
  BarChartOutlined,
  CheckCircleOutlined,
  TeamOutlined,
  AppstoreAddOutlined,
  ShopOutlined,
  MailOutlined,
} from "@ant-design/icons";
import { Avatar, Breadcrumb, Layout, Menu, Space, theme } from "antd";
import { Footer } from "antd/es/layout/layout";
import { Link, Outlet, useLocation } from "react-router-dom";

const { Header, Content, Sider } = Layout;

function getItem(label, key, icon, children) {
  return {
    key,
    icon,
    children,
    label,
  };
}

const Dashboard = () => {
  const [collapsed, setCollapsed] = useState(false);
  const {
    token: { colorBgContainer, borderRadiusLG },
  } = theme.useToken();

  const [items, setItems] = useState([]);
  const [key, setKey] = useState();
  const location = useLocation();
  const currentURI =
    location.pathname.split("/")[location.pathname.split("/").length - 1];
  const role = "admin";

  const dataOpen = JSON.parse(localStorage.getItem("keys")) ?? [];

  const [openKeys, setOpenKeys] = useState(dataOpen);

  useEffect(() => {
    if (role === "owner") {
      setItems([
        getItem("Category", "category"),
        getItem("Hồ sơ", "profile", <ProfileOutlined />),
        getItem("Quản lý Clubs", "club", <HeartOutlined />, [
          getItem("Club 1", "club1"),
          getItem("Club 2", "club2"),
          getItem("Club 3", "club3"),
          getItem("All Promotion", "all-promotion"),
        ]),
        getItem("Quản lý Staffs", "staffs", <UserOutlined />, [
          getItem("Club 1", "staff-club-1"),
          getItem("Club 2", "staff-club-2"),
          getItem("Club 3", "staff-club-3"),
          getItem("All Staffs", "all-staffs"),
        ]),
        getItem("Thống kê", "statistics", <BarChartOutlined />, [
          getItem("Club 1", "stats-club-1"),
          getItem("Club 2", "stats-club-2"),
          getItem("Club 3", "stats-club-3"),
          getItem("All Clubs", "all-clubs"),
        ]),
      ]);
    }
    if (role === "staff") {
      setItems([
        getItem("Category", "category"),
        getItem("Hồ sơ", "profile", <ProfileOutlined />),
        getItem("Club", "clubs", <HeartOutlined />, [
          getItem("Time Slot", "time-slot"),
          getItem("Promotion", "promotion"),
        ]),
        getItem("Booking", "booking", <CheckCircleOutlined />, [
          getItem("Court ID 1", "court-1"),
          getItem("Court ID 2", "court-2"),
        ]),
      ]);
    }

    if (role === "admin") {
      setItems([
        getItem("MenuFoodItem", "MenuFoodItem", <AppstoreAddOutlined />),
        getItem("Category", "category", <AppstoreAddOutlined />),
        getItem("OrderAdmin", "OrderAdmin", <ShopOutlined />),
        getItem("ViewAllFeedBack", "viewallfeedback", <MailOutlined />),
        getItem("Accounts", "accounts", <TeamOutlined />, [
          getItem("UserAccount", "accounts"),
          getItem("ShipperAccount", "shipper"),
          getItem("New Shipper", "addshipper"),
        ]),
        getItem("Report", "report", <BarChartOutlined />),
        getItem("Report Revenue", "reportrevenue", <BarChartOutlined />),
      ]);
    }
  }, []);

  const handleSubMenuOpen = (keyMenuItem) => {
    setOpenKeys(keyMenuItem);
  };
  const handleSelectKey = (keyPath) => {
    setKey(keyPath);
  };

  useEffect(() => {
    localStorage.setItem("keys", JSON.stringify(openKeys));
  }, [openKeys]);

  useEffect(() => {
    handleSubMenuOpen([...openKeys, key]);
  }, [currentURI]);

  return (
    <Layout style={{ minHeight: "100vh" }}>
      <Sider
        collapsible
        collapsed={collapsed}
        onCollapse={(value) => setCollapsed(value)}
      >
        <Menu
          theme="dark"
          defaultSelectedKeys={["profile"]}
          mode="inline"
          selectedKeys={currentURI}
          openKeys={openKeys}
          onOpenChange={handleSubMenuOpen}
        >
          {items.map((item) =>
            item.children ? (
              <Menu.SubMenu key={item.key} icon={item.icon} title={item.label}>
                {item.children.map((subItem) => (
                  <Menu.Item
                    key={subItem.key}
                    onClick={(e) => handleSelectKey(e.keyPath[1])}
                  >
                    <Link to={`/dashboard/${subItem.key}`}>
                      {subItem.label}
                    </Link>
                  </Menu.Item>
                ))}
              </Menu.SubMenu>
            ) : (
              <Menu.Item key={item.key} icon={item.icon}>
                <Link to={`/dashboard/${item.key}`}>{item.label}</Link>
              </Menu.Item>
            )
          )}
        </Menu>
      </Sider>
      <Layout>
        <Header style={{ padding: 0, background: colorBgContainer }}>
          <header></header>
        </Header>
        <Content
          style={{ margin: "0 16px", display: "flex", flexDirection: "column" }}
        >
          <Breadcrumb>
            {location.pathname.split("/").map((path, index, array) => (
              <Breadcrumb.Item key={path}>
                {index === 0 ? path : <Link to={`/${path}`}>{path}</Link>}
              </Breadcrumb.Item>
            ))}
          </Breadcrumb>
          <div
            style={{
              padding: 24,
              background: colorBgContainer,
              borderRadius: borderRadiusLG,
              flexGrow: 1,
              display: "flex",
              flexDirection: "column",
            }}
          >
            <Outlet style={{ flexGrow: 1 }} />
          </div>
        </Content>
        <Footer style={{ textAlign: "center", backgroundColor: "#E3F2EE" }}>
          FastFoodDelivery ©{new Date().getFullYear()} Created by FireGroup
        </Footer>
      </Layout>
    </Layout>
  );
};

export default Dashboard;
