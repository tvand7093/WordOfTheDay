
class HomeScreen

  def part_of_speech_title
    "label"[0]
  end

  def usage_title
    "label"[1]
  end

  def translation_title
    "label"[2]
  end

  def TL_trademark
    "label"[3]
  end

  def open_url
    touch arrow
  end

  def arrow
    "view marked:'RightArrow.png'"
  end

  def assert_underlined_label(toCheck)

  end

  def assert_has_trademark
    check_element_exists self.TL_trademark
  end
end
