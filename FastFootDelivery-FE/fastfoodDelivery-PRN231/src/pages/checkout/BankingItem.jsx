import { useState } from 'react';
import './BankingItem.scss';

const BankingItems = ({ value, url }) => {
  const [selectedBanking, setSelectedBanking] = useState('');

  const handleChangeBanking = (e) => {
    setSelectedBanking(e.target.value);
  };

  return (
    <div className='banking-item-container'>
      <input
        type='radio'
        name='online-banking'
        value={value}
        className='banking-radio'
        onChange={handleChangeBanking}
      />
      <img src={url} alt='banking-online' className='banking-image' />
    </div>
  );
};

export default BankingItems;
