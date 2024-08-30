import { Anchor, Flex, Menu } from 'antd';
import './index.scss';
import { useEffect, useState } from 'react';
import BankingItems from './BankingItem';
import Typography from 'antd/es/typography/Typography';
import { useLocation, useNavigate } from 'react-router-dom';
import axios from 'axios';

const paymentMethods = [
  {
    key: 'atm',
    label: 'Chuyển khoản ATM',
  },
  {
    key: 'cod',
    label: 'Thanh toán COD',
  },
  {
    key: 'online',
    label: 'Thanh toán online',
  },
];

const bankingList = [
  {
    value: 'eximbank',
    url: '/bank-image/eximbank.png',
  },
  {
    value: 'vietcombank',
    url: '/bank-image/vietcombank.png',
  },
  {
    value: 'dongabank',
    url: '/bank-image/dongabank.png',
  },
  {
    value: 'techcombank',
    url: '/bank-image/techcombank.png',
  },
  {
    value: 'mbbank',
    url: '/bank-image/mbbank.png',
  },
  {
    value: 'vibbank',
    url: '/bank-image/vibbank.png',
  },
  {
    value: 'viettinbank',
    url: '/bank-image/viettinbank.png',
  },
  {
    value: 'acbbank',
    url: '/bank-image/acbbank.png',
  },
];

const Checkout = () => {
  const [selectedMethod, setSelectedMethod] = useState('online');
  const [amount, setAmount] = useState(10000);
  const [orderDescription, setOrderDescription] = useState('blabla');
  const [paymentStatus, setPaymentStatus] = useState('');
  const location = useLocation();

  useEffect(() => {
    const queryParams = new URLSearchParams(location.search);
    const vnpResponseCode = queryParams.get('vnp_ResponseCode');

    if (vnpResponseCode === '00') {
      setPaymentStatus('Thanh toán thành công!');
    } else {
      setPaymentStatus('Thanh toán thất bại!');
    }
  }, [location.search]);

  return (
    <Flex className='checkout-container'>
      {/* <Anchor affix={false} items={paymentMethods} /> */}
      <div
        style={{
          display: 'flex',
          flexDirection: 'column',
        }}
      >
        <div className='checkout-title'>PHƯƠNG THỨC THANH TOÁN</div>
        <div style={{ display: 'flex' }}>
          <div
            style={{
              width: 256,
            }}
          >
            <Menu
              defaultSelectedKeys={['online']}
              mode='vertical'
              theme='light'
              items={paymentMethods}
              onSelect={({ key }) => {
                setSelectedMethod(key);
              }}
            />
          </div>
          <div className='banking-container'>
            {selectedMethod === 'online' && (
              <>
                <Typography.Text className='banking-text'>
                  Tài khoản ngân hàng của Quý khách cần đăng ký dịch vụ Internet
                  Banking.
                </Typography.Text>
                <div className='banking-list'>
                  {bankingList.map((bank) => {
                    return (
                      <BankingItems
                        key={bank.value}
                        value={bank.value}
                        url={bank.url}
                      />
                    );
                  })}
                </div>
              </>
            )}
          </div>
        </div>
        <a
          className='checkout-btn'
          href={`https://localhost:7173/api/VNPay/payment/${amount}/${orderDescription}`}
        >
          XÁC NHẬN THANH TOÁN
        </a>
      </div>
      <p>{paymentStatus}</p>
    </Flex>
  );
};

export default Checkout;
