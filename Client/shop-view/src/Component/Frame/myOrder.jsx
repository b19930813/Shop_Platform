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
  }
}));

export default function OrderList(props) {
  const [expanded, setExpanded] = React.useState(false);
  const classes = useStyles();
  const handleChange = (panel) => (event, isExpanded) => {
    setExpanded(isExpanded ? panel : false);
  };

  React.useEffect(() => {
    axios.get('api/Order/GetOrderByUserId', config)
      .then(response => {
        console.log(response)
      })
  }, [])

  return (
    <div className={classes.context}>
      <h1 style={{ 'display': 'inline-block' }}>訂單資訊</h1>
      <Accordion expanded={expanded === 'panel1'} onChange={handleChange('panel1')}>
        <AccordionSummary
          expandIcon={<ExpandMoreIcon />}
          aria-controls="panel1bh-content"
          id="panel1bh-header"
        >
          <Typography sx={{ width: '33%', flexShrink: 0 }}>
            訂單編號:{}
          </Typography>
          <Typography sx={{ width: '33%', flexShrink: 0 }}>訂單狀態:{}</Typography>
          <Typography sx={{ width: '33%', flexShrink: 0 }}>訂單金額:{}元</Typography>
        </AccordionSummary>
        <AccordionDetails>
          <Typography>
            放圖片跟內容
          </Typography>
        </AccordionDetails>
      </Accordion>

      <Stack spacing={2}>
        <Pagination count={10} color="secondary" className={classes.pageCenter} />
      </Stack>
    </div>
  );
}
