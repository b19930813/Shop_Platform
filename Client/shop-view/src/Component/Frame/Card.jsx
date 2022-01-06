import * as React from 'react';
import { styled } from '@mui/material/styles';
import Card from '@mui/material/Card';
import CardHeader from '@mui/material/CardHeader';
import CardMedia from '@mui/material/CardMedia';
import CardContent from '@mui/material/CardContent';
import CardActions from '@mui/material/CardActions';
import Collapse from '@mui/material/Collapse';
import Avatar from '@mui/material/Avatar';
import IconButton from '@mui/material/IconButton';
import Typography from '@mui/material/Typography';
import { red } from '@mui/material/colors';
import FavoriteIcon from '@mui/icons-material/Favorite';
import ShareIcon from '@mui/icons-material/Share';
import ExpandMoreIcon from '@mui/icons-material/ExpandMore';
import MoreVertIcon from '@mui/icons-material/MoreVert';
import image from '../image/mouse.jpg'
import { makeStyles } from '@material-ui/core/styles';
import CardActionArea from '@material-ui/core/CardActionArea';

const useStyles = makeStyles({
    card: {
        width: 220,
        display: "inline-block",
        marginRight: "15px",
    },
    media: {
        height: 140,
    },
    subContent: {
        height: 80,

    },
    main: {
        display: "inline-block",

    },
    media: {
        height: 0,
        paddingTop: '56.25%', // 16:9,
        marginTop: '30'
    }
});


const ExpandMore = styled((props) => {
    const classes = useStyles();
    const { expand, ...other } = props;
    return <IconButton {...other} />;
})(({ theme, expand }) => ({
    transform: !expand ? 'rotate(0deg)' : 'rotate(180deg)',
    marginLeft: 'auto',
    transition: theme.transitions.create('transform', {
        duration: theme.transitions.duration.shortest,
    }),
}));

export default function RecipeReviewCard(props) {
    const classes = useStyles();
    const [pic, setPic] = React.useState("../image/mouse.jpg")
    

    const handleCardClick = () =>{
        document.location.href = `/Commodity?CommodityId=${props.Data.Id}&StoreId=${props.Data.StoreId}`;
        console.log(props)
    }

    return (
        <Card sx={{ maxWidth: 250 }} className={classes.main}>
            <CardHeader
                avatar={
                    <Avatar sx={{ bgcolor: red[500] }} aria-label="recipe">
                        F
                    </Avatar>
                }
                action={
                    <IconButton aria-label="settings">
                        <MoreVertIcon />
                    </IconButton>
                }
                title={props.Data.Name}
                subheader="September 14, 2016"
            />
            <CardActionArea>
                <CardMedia
                    component="img"
                    height="150"
                    image={props.Data.ImagePath}
                    onClick={handleCardClick}
                />
                <CardContent>
                    <Typography variant="body2" color="text.secondary">
                        This impressive paella is a perfect party dish and a fun meal to cook

                    </Typography>
                </CardContent>
            </CardActionArea>
            <CardActions disableSpacing>
                <IconButton aria-label="add to favorites">
                    <FavoriteIcon />
                </IconButton>
                <IconButton aria-label="share">
                    <ShareIcon />
                </IconButton>
            </CardActions>

        </Card>
    );
}
