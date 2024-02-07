import { useState, useEffect } from "react";
import { useIntl } from "react-intl";
import { configService } from "@pages/background/services/config";
import { useLocale } from "@src/_locales";
import { isValidUrl } from "@pages/background/utils";
import { Text } from "@components/ui";
import { CustomSwitch } from "@components/customSwitch";

// This screen should not be styled with theme as it does not depend on the vendor configuration
export function Config(props): JSX.Element {
  const [vendorUrl, setVendorUrl] = useState("");
  const [urlError, setUrlError] = useState("");
  const { formatMessage } = useIntl();
  const { changeLocale, currentLocale } = useLocale();
  const validUrlMsg = formatMessage({ id: "config.error.enterUrl" });
  const invalidUrlError = formatMessage({ id: "config.error.invalidUrl" });

  const getVendorUrl = async () => {
    const _vendorUrl = await configService.getUrl();
    setVendorUrl(_vendorUrl);
  };

  useEffect(() => {
    getVendorUrl();
  }, []);

  const onBlurUrl = () => {
    if (!vendorUrl || !isValidUrl(vendorUrl)) {
      setUrlError(validUrlMsg);
    } else {
      setUrlError("");
    }
  };

  const handleSetUrl = async () => {
    let hasError = false;
    if (!vendorUrl || !isValidUrl(vendorUrl)) {
      setUrlError(validUrlMsg);
      hasError = true;
    }
    try {
      const resp = await (await fetch(vendorUrl)).json();
      await configService.setData(resp);
      const imageBlob = await fetch(resp?.icon).then((r) => r.blob());
      const bitmap = await createImageBitmap(imageBlob);
      const canvas = new OffscreenCanvas(bitmap.width, bitmap.height);
      const context = canvas.getContext("2d");
      context?.drawImage(bitmap, 0, 0);
      const imageData = context?.getImageData(
        0,
        0,
        bitmap.width,
        bitmap.height
      );
      chrome.action.setIcon({ imageData: imageData });
    } catch (error) {
      setUrlError(invalidUrlError);
      hasError = true;
    }
    if (!hasError) {
      await configService.setUrl(vendorUrl);
      props.afterSetUrl();
    }
  };

  return (
    <>
      <div className="px-4 py-2">
        <p className="text-sm font-bold">
          {formatMessage({ id: "config.vendorUrl.label" })}
        </p>
        <input
          type="text"
          id="vendor_url"
          className={`border text-black text-sm rounded-lg block w-full p-2.5 ${
            urlError ? " text-red border-red" : ""
          } `}
          placeholder={formatMessage({ id: "config.vendorUrl.placeholder" })}
          required
          value={vendorUrl}
          onChange={(e) => setVendorUrl(e.target.value)}
          onBlur={onBlurUrl}
        />
        {urlError ? <p className="text-red">{urlError}</p> : null}
      </div>
      <div className="px-4 py-2">
        <Text className="font-bold" $color="heading">
          Select Spanish
        </Text>
        <CustomSwitch
          isChecked={currentLocale == "es"}
          handleToggle={() => {
            changeLocale(currentLocale == "es" ? "en" : "es");
          }}
        />
      </div>
      <div className="flex flex-row justify-center">
        <button
          onClick={handleSetUrl}
          className="text-white bg-green p-2 flex flex-row focus:outline-none font-medium rounded-full text-sm"
        >
          <p className="font-medium text-md">
            {formatMessage({ id: "action.save" })}
          </p>
        </button>
      </div>
    </>
  );
}
