import { Helmet } from 'react-helmet-async';

const Seo = ({ title, description }) => (
  <Helmet>
    <title>{title ? `${title} | آکادمی پردیس توس` : 'آکادمی پردیس توس'}</title>
    {description && <meta name="description" content={description} />}
    <meta name="language" content="fa" />
    <meta name="robots" content="index,follow" />
    <meta property="og:title" content={title || 'آکادمی پردیس توس'} />
    {description && <meta property="og:description" content={description} />}
  </Helmet>
);

export default Seo;
