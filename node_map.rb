require 'ruby-debug'
class NodeMap
  def initialize cells
    @cells = cells
  end

  def input_nodes x, y
    nodes = []
    siblings = self.siblings x, y
    nodes << siblings['right-up'] if siblings['right-up'].direction == 'left-down'
    nodes << siblings['left-up'] if siblings['left-up'].direction == 'right-down'
    nodes << siblings['right-down'] if siblings['right-down'].direction == 'right-up'
    nodes << siblings['left-down'] if siblings['left-down'].direction == 'left-up'
    nodes
  end

  def find_all_next_nodes x,y
    nodes = []
    while true do
      next_nodes = output_nodes(x, y)
      if next_nodes.empty?
        return nodes
      end
      nodes << next_nodes.first
      x = next_nodes.first.x
      y = next_nodes.first.y
    end
  end

  def output_nodes x, y
    nodes = []
    return nodes if self[x,y].direction.nil?
    siblings = self.siblings x,y
    nodes << siblings[self[x,y].direction] unless siblings.nil?
    nodes
  end
  def [](x,y)
    @cells[x][y]
  end


  def siblings x, y
    return nil if x > 9 || y>9
    if y % 2 ==0
      result =  {
      'left-up' => self[x-1, y-1],
      'left-down' => self[x-1, y+1],
      'right-up' => self[x, y-1],
      'right-down' => self[x, y+1],
      }
    else
      result = {
      'left-up' => self[x, y-1],
      'left-down' => self[x, y+1],
      'right-up' => self[x+1, y-1],
      'right-down' => self[x+1, y+1],
    }
    end
    result
  end

end
class Cell
  attr_accessor :direction, :x, :y
  def initialize x, y
    @x, @y = x, y
  end
end
def array
  a = []
  (0..10).each do |x|
    a << []
    (0..10).each do |y|
      a[x] << Cell.new(x, y)
    end
  end
  a
end

map = NodeMap.new array

map[2,0].direction= 'left-down'
map[1,1].direction= 'right-down'
map[2,2].direction= 'left-down'
map[1,3].direction= 'left-down'
map[1,4].direction= 'right-down'
map[1,5].direction= 'right-down'

p map.find_all_next_nodes(2,0)