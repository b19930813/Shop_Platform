import * as React from 'react';
import Accordion from '@mui/material/Accordion';
import AccordionDetails from '@mui/material/AccordionDetails';
import AccordionSummary from '@mui/material/AccordionSummary';
import Typography from '@mui/material/Typography';
import ExpandMoreIcon from '@mui/icons-material/ExpandMore';
import { makeStyles } from '@material-ui/core';
import Pagination from '@mui/material/Pagination';
import Stack from '@mui/material/Stack';
import Button from '@mui/material/Button';
import axios from 'axios';
import { config } from '../../api/config'

const useStyles = makeStyles(theme => ({
    context: {
        paddingTop: "2%",
        paddingLeft: "12%",
        paddingRight: "12%",
    },
    pageCenter: {
        margin:'auto',
        paddingTop: "2%"
    }
}));

export default function OrderList() {
  const [expanded, setExpanded] = React.useState(false);
  const classes = useStyles();
  const handleChange = (panel) => (event, isExpanded) => {
    setExpanded(isExpanded ? panel : false);
  };

  const [commodity, setCommodity] = React.useState({
    Name: "Apple",
    Classification: "水果",
    Describe: "水果",
    Price: 100,
    ImagePath: null
  })

  const [Order, setOrder] = React.useState({
    OrderId: "110",
    Commodities : commodity
  });
  
  const handleAddClick = () =>{
    axios.post('api/Order/AddOrder', Order, config)
    .then(response => {
        console.log(response)
    })
    console.log("新增訂單")
  }

  const handleQueryClick = () =>{
    axios.get('api/Order/GetOrder', Order, config)
    .then(response => {
        console.log(response)
    })
    console.log("查詢訂單")
  }

  return (
    <div className = {classes.context}>
        <h1  style={{ 'display': 'inline-block' }}>訂單資訊</h1>
        <Button color="warning" variant="contained" style={{ 'float': 'right' , 'marginTop' : '3%'}} onClick={handleAddClick} >加入訂單</Button>
        <Button color="warning" variant="contained" style={{ 'float': 'right' , 'marginTop' : '3%'}} onClick={handleQueryClick} >訂單資訊</Button>
      <Accordion expanded={expanded === 'panel1'} onChange={handleChange('panel1')}>
        <AccordionSummary
          expandIcon={<ExpandMoreIcon />}
          aria-controls="panel1bh-content"
          id="panel1bh-header"
        >
          <Typography sx={{ width: '33%', flexShrink: 0 }}>
            滑鼠
          </Typography>
          <Typography sx={{ color: 'text.secondary' }}>價格 : 150</Typography>
        </AccordionSummary>
        <AccordionDetails>
          <Typography>
              放圖片跟內容
          </Typography>
        </AccordionDetails>
      </Accordion>

      <Stack spacing={2}>
      <Pagination count={10} color="secondary" className = {classes.pageCenter} />
      </Stack>
    </div>
  );
}
