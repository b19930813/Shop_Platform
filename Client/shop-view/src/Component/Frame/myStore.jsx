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
    margin: 'auto',
    paddingTop: "2%"
  },
  imageClass: {
    width: '200px',
    height: '200px',
    marginLeft: '50%',
    display: 'flex',
  },
  Text: {

  }
}));

function createData(Name, Desc, Count, Price, Image) {
  return { Name, Desc, Count, Price, Image };
}

export default function ControlledAccordions() {
  const [expanded, setExpanded] = React.useState(false);
  const classes = useStyles();
  const storeId = parseInt(localStorage.getItem("storeId"));
  const userName = localStorage.getItem("userName");
  const handleChange = (panel) => (event, isExpanded) => {
    setExpanded(isExpanded ? panel : false);
  };

  const handleAddClick = () => {
    window.open('/AddCommodity')
  }

  const handleCreateStoreClick = () => {
    window.open('/CreateStore')
  }

  const [commodity, setCommodity] = React.useState({
    Name: "",
    Desc: "",
    Count: 0,
    Price: 0,
    Image: ""
  })

  const [rows, setRows] = React.useState([
  ])

  React.useEffect(() => {
    if (storeId=="")
    {
      return;
    }

    axios.get('api/Store/GetCommodityByStoreId/' + storeId, config)
      .then(response => {
        console.log(response)
        var tempData = []

        response.data.forEach(r => {
          tempData.push(r)
        });
        setRows(tempData);
        console.log("tempData", tempData);
        console.log("rows", rows);
      })
  }, [])

  return (
    <div className={classes.context}>
      <div>
        <h1 style={{ 'display': 'inline-block' }}>{`${userName}的商店`}</h1>
        <Button color="warning" variant="contained" style={{ 'float': 'right', 'marginTop': '3%' }} onClick={handleCreateStoreClick} >建立賣場</Button>
        <Button color="warning" variant="contained" style={{ 'float': 'right', 'marginTop': '3%' }} onClick={handleAddClick} >加入商品</Button>
      </div>
      {rows.map((row) => (
        <Accordion expanded={expanded === 'panel1'} onChange={handleChange('panel1')}>
          <AccordionSummary
            expandIcon={<ExpandMoreIcon />}
            aria-controls="panel1bh-content"
            id="panel1bh-header"
          >
            <Typography sx={{ width: '33%', flexShrink: 0 }}>
              {`商品名稱 : ${row.name}`}
            </Typography>
            <Typography sx={{ width: '33%', flexShrink: 0 }}>{`商品價格 : ${row.price} 元`}</Typography>
            <Typography sx={{ width: '33%', flexShrink: 0 }}>{`庫存數量 : 1`}</Typography>
          </AccordionSummary>
          <AccordionDetails>
            <Typography>
              <div className={classes.Text}>
                <p>{`商品敘述 : ${row.describe}`}</p>
              </div>
              <div className={classes.imageClass}>
                <img src={`https://localhost:44387/api/User/getImage/${row.imagePath}`} style={{ "width": "150px", "height": "150px" }} />
              </div>

            </Typography>
          </AccordionDetails>
        </Accordion>
        ))}
      <Stack spacing={2}>
        <Pagination count={10} color="secondary" className={classes.pageCenter} />
      </Stack>
    </div>
  );
}
