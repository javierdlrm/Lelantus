import time
from grove.button import Button
from grove.factory import Factory
import subprocess
import uuid
from azure.storage.blob import BlockBlobService, PublicAccess

__all__ = ["GroveButton"]


class GroveButton(object):
    """
    Grove Button class

    Args:
        pin(int): the number of gpio/slot your grove device connected.
    """

    def __init__(self, pin):
        # High = pressed
        self.__btn = Factory.getButton("GPIO-HIGH", pin)
        self.__last_time = time.time()
        self.__on_press = None
        self.__on_release = None
        self.__btn.on_event(self, GroveButton.__handle_event)

    @property
    def on_press(self):
        """
        Property access with
            callback -- a callable function/object,
                        will be called when there is a button pressing.
            callback prototype:
                callback()
                Returns: none

        Examples:
            set

            .. code-block:: python

                obj.on_press = callback

            get

            .. code-block:: python

                callobj = obj.on_press
        """
        return self.__on_press

    @on_press.setter
    def on_press(self, callback):
        if not callable(callback):
            return
        self.__on_press = callback

    @property
    def on_release(self):
        """
        Property access with
            callback -- a callable function/object,
                        will be called when there is a button releasing.
            callback prototype:
                callback()
                Returns: none

        Examples:
            set

            .. code-block:: python

                obj.on_release = callback

            get

            .. code-block:: python

                callobj = obj.on_release
        """
        return self.__on_release

    @on_release.setter
    def on_release(self, callback):
        if not callable(callback):
            return
        self.__on_release = callback

    def __handle_event(self, evt):
        dt, self.__last_time = evt["time"] - self.__last_time, evt["time"]
        # print("event index:{} event:{} pressed:{}"
        #      .format(evt["index"], evt["code"], evt["pressed"]))
        if evt["code"] == Button.EV_LEVEL_CHANGED:
            if evt["pressed"]:
                if callable(self.__on_press):
                    self.__on_press(dt)
            else:
                if callable(self.__on_release):
                    self.__on_release(dt)


Grove = GroveButton


def main():
    from grove.helper import SlotHelper

    sh = SlotHelper(SlotHelper.GPIO)
    pin = sh.argv2pin()

    button = GroveButton(pin)

    block_blob_service = BlockBlobService(
        account_name="lelantussa",
        account_key="Lb7K36gtXNnJWjFA1FzedR8NO0LJ3yppGQzbmoZGuR3naqY1dyx0T68s2jQnszEannN7LdgFcAJcd8MfEf0Kfw==",
    )
    container_name = "lelantus-sa-container/4918ae98-2c60-4536-aae4-a471b0bfc962"

    def on_press(t):
        print("Button is pressed")
        print("Recording video started")
        file_name = str(uuid.uuid4()) + ".avi"
        full_path = "/home/pi/media/" + file_name
        subprocess.call(
            [
                "ffmpeg",
                "-f",
                "v4l2",
                "-framerate",
                "30",
                "-s",
                "640x480",
                "-t",
                "10",
                "-i",
                "/dev/video0",
                "media/" + file_name,
            ]
        )
        print("Recording video finished")
        print("\nUploading to Blob storage as blob: " + file_name)
        block_blob_service.create_blob_from_path(container_name, file_name, full_path)

    def on_release(t):
        print("Button is released, pressed for {0} seconds".format(round(t, 6)))

    button.on_press = on_press
    button.on_release = on_release

    while True:
        time.sleep(1)


if __name__ == "__main__":
    main()
